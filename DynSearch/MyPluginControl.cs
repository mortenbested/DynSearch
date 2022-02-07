using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace DynSearch
{
    public partial class MyPluginControl : PluginControlBase, IStatusBarMessenger
    {
        private IEnumerable<CrmObject> crmObjects; //All CRM metadata
        private string entityFilter = ""; //Specific entity is used as filter
        private Type typeFilter = null; //Specific object type is used as filter (Entity, Action, etc.)
        private string searchTerm = ""; //Specific search term is used

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar; //From IStatusBarMessenger interface

        public MyPluginControl()
        {
            InitializeComponent();
            this.DisableInputFields(); //Until metadata is loaded
            this.comboBoxTypeFilter.DataSource = this.GetTypeFilterItems();
            this.SetImagesOnResultListView();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
        }

        //When connection is changed in XrmToolbox
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            this.crmObjects = null;
            this.DisableInputFields();
        }

        //When used click on "load data from crm"
        private void ToolStripButtonLoadData_Click(object sender, EventArgs e)
        {
            ExecuteMethod(this.LoadMetaData);
        }

        //Search metadata using the current filters and display the result
        private void DoSearchAndShowResults()
        {
            IEnumerable<CrmObject> results = Searcher.Search(this.searchTerm, this.entityFilter, this.typeFilter, this.crmObjects);

            results = results.OrderBy(crmObject => {
                if (crmObject is CrmSolution) return 1;
                if (crmObject is CrmEntity) return 2;
                if (crmObject is CrmWorkflow) return 3;
                if (crmObject is CrmAction) return 4;
                if (crmObject is CrmPluginStep) return 5;
                if (crmObject is CrmSecurityRole) return 6;
                if (crmObject is CrmWebResource) return 7;
                if (crmObject is CrmBusinessRule) return 8;
                if (crmObject is CrmAttribute) return 9;
                else return 10;
            }).ThenBy(crmObject => crmObject.DisplayName);

            IEnumerable<ListViewItem> listViewItems = results.Select(result =>
            {
                ResultItem resultItem = ResultItem.Get(result);
                return resultItem.AsListViewItem();
            });

            this.listViewResults.Items.Clear();
            this.listViewResults.Items.AddRange(listViewItems.ToArray());
        }

        //Fetch metadata from CRM
        private void LoadMetaData()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting metadata",
                Work = (worker, args) =>
                {
                    MetadataLoader metadataLoader = new MetadataLoader(this.Service, this.StatusChangeHandler);
                    args.Result = metadataLoader.LoadAll();
                    SendMessageToStatusBar(this, new StatusBarMessageEventArgs(string.Empty));
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.crmObjects = args.Result as List<CrmObject>;
                    this.EnableInputFields();
                    this.AddItemsToEntityFilter();
                }
            });
        }

        //When metadata loader changes status, update progress bar
        void StatusChangeHandler(int percentageDone, string statusText)
        {
            SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(percentageDone, statusText));
        }

        //Enable input fields (when crm data is ready)
        private void EnableInputFields()
        {
            this.textBoxSearchTerm.Enabled = true;
            this.labelEntityFilter.Visible = true;
            this.labelFilterByType.Visible = true;
            this.comboBoxEntityFilter.Visible = true;
            this.comboBoxTypeFilter.Visible = true;
            this.panelObjectFilter.Visible = true;
            this.listViewResults.Enabled = true;
        }

        //Disable input fields (when crm data is not ready)
        private void DisableInputFields()
        {
            this.textBoxSearchTerm.Enabled = false;
            this.labelEntityFilter.Visible = false;
            this.labelFilterByType.Visible = false;
            this.comboBoxEntityFilter.Visible = false;
            this.comboBoxTypeFilter.Visible = false;
            this.panelObjectFilter.Visible = false;
            this.listViewResults.Enabled = false;
        }

        //Populate the entity filter combobox with entity names (called when metadata has been loaded)
        private void AddItemsToEntityFilter()
        {
            IEnumerable<string> entityNames = this.crmObjects
                .Where(crmObject => crmObject is CrmEntity entity)
                .Where(crmObject => !string.IsNullOrEmpty(crmObject.EntityDisplayName))
                .Select(crmObject => crmObject.EntityDisplayName)
                .OrderBy(name => name);

            this.comboBoxEntityFilter.Items.Clear();
            this.comboBoxEntityFilter.Items.Add("");
            this.comboBoxEntityFilter.Items.AddRange(entityNames.ToArray());
        }

        //When search term textfield is changed => update search
        private void TextBoxSearchTerm_TextChanged(object sender, EventArgs e)
        {
            this.searchTerm = textBoxSearchTerm.Text;
            this.DoSearchAndShowResults();
        }

        //When type filter is changed => update search
        private void ComboBoxTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (TypeFilterItem)comboBoxTypeFilter.SelectedItem;
            this.typeFilter = selected.Type;
            this.DoSearchAndShowResults();
        }

        //When entity filter is selected => update search
        private void ComboBoxEntityFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.entityFilter = this.comboBoxEntityFilter.SelectedItem.ToString();
            this.DoSearchAndShowResults();
        }

        //When text in entity filter is changed
        //Remove entity filter if text in combobox is removed
        private void ComboBoxEntityFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.comboBoxEntityFilter.Text))
            {
                this.entityFilter = null;
                this.DoSearchAndShowResults();
            }
        }

        //When result row is double clicked => open browser and navigate to item
        private void ListViewResults_DoubleClick(object sender, EventArgs e)
        {
            ResultItem selectedItem = (ResultItem)this.listViewResults.SelectedItems[0].Tag;
            if (!string.IsNullOrEmpty(selectedItem.Url))
            {
                string link = this.ConnectionDetail.WebApplicationUrl + selectedItem.Url;
                ProcessStartInfo sInfo = new ProcessStartInfo(link);
                Process.Start(sInfo);
            }
            else
            {
                MessageBox.Show("Not able to navigate to items of this type");
            }
        }

        private List<TypeFilterItem> GetTypeFilterItems()
        {
            return new List<TypeFilterItem>()
            {
                new TypeFilterItem()
                {
                    Name = "",
                    Type = null
                },
                new TypeFilterItem()
                {
                    Name = "Action",
                    Type = typeof(CrmAction)
                },
                new TypeFilterItem()
                {
                    Name = "Business Rule",
                    Type = typeof(CrmBusinessRule)
                },
                new TypeFilterItem()
                {
                    Name = "Entity",
                    Type = typeof(CrmEntity)
                },
                new TypeFilterItem()
                {
                    Name = "Field",
                    Type = typeof(CrmAttribute)
                },
                new TypeFilterItem()
                {
                    Name = "Plugin Step",
                    Type = typeof(CrmPluginStep)
                },
                new TypeFilterItem()
                {
                    Name = "Security Role",
                    Type = typeof(CrmSecurityRole)
                },
                new TypeFilterItem()
                {
                    Name = "Solution",
                    Type = typeof(CrmSolution)
                },
                new TypeFilterItem()
                {
                    Name = "Web Resource",
                    Type = typeof(CrmWebResource)
                },
                new TypeFilterItem()
                {
                    Name = "Workflow",
                    Type = typeof(CrmWorkflow)
                }
            };
        }

        private void SetImagesOnResultListView()
        {
            ImageList imageList = new ImageList();
            foreach (Image img in Icons.GetIcons())
            {
                imageList.Images.Add(img);
            }
            listViewResults.SmallImageList = imageList;
        }
    }
}