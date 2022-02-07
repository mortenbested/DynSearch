using System.Windows.Controls;
using System.Windows.Forms;

namespace DynSearch
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLoadData = new System.Windows.Forms.ToolStripButton();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.comboBoxEntityFilter = new System.Windows.Forms.ComboBox();
            this.labelEntityFilter = new System.Windows.Forms.Label();
            this.labelFilterByType = new System.Windows.Forms.Label();
            this.panelObjectFilter = new System.Windows.Forms.Panel();
            this.comboBoxTypeFilter = new System.Windows.Forms.ComboBox();
            this.textBoxSearchTerm = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.objectTypeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logicalNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.entityColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.infoColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripMenu.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.panelObjectFilter.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoadData});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(955, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // toolStripButtonLoadData
            // 
            this.toolStripButtonLoadData.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripButtonLoadData.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoadData.Image")));
            this.toolStripButtonLoadData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadData.Name = "toolStripButtonLoadData";
            this.toolStripButtonLoadData.Size = new System.Drawing.Size(172, 28);
            this.toolStripButtonLoadData.Text = "Load metadata from CRM";
            this.toolStripButtonLoadData.Click += new System.EventHandler(this.ToolStripButtonLoadData_Click);
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.comboBoxEntityFilter);
            this.groupBoxSearch.Controls.Add(this.labelEntityFilter);
            this.groupBoxSearch.Controls.Add(this.labelFilterByType);
            this.groupBoxSearch.Controls.Add(this.panelObjectFilter);
            this.groupBoxSearch.Controls.Add(this.textBoxSearchTerm);
            this.groupBoxSearch.Controls.Add(this.labelSearch);
            this.groupBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSearch.Location = new System.Drawing.Point(16, 40);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(919, 72);
            this.groupBoxSearch.TabIndex = 5;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Search and filtering";
            // 
            // comboBoxEntityFilter
            // 
            this.comboBoxEntityFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxEntityFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxEntityFilter.FormattingEnabled = true;
            this.comboBoxEntityFilter.Location = new System.Drawing.Point(445, 34);
            this.comboBoxEntityFilter.Name = "comboBoxEntityFilter";
            this.comboBoxEntityFilter.Size = new System.Drawing.Size(191, 21);
            this.comboBoxEntityFilter.TabIndex = 2;
            this.comboBoxEntityFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxEntityFilter_SelectedIndexChanged);
            this.comboBoxEntityFilter.TextChanged += new System.EventHandler(this.ComboBoxEntityFilter_TextChanged);
            // 
            // labelEntityFilter
            // 
            this.labelEntityFilter.AutoSize = true;
            this.labelEntityFilter.Location = new System.Drawing.Point(365, 37);
            this.labelEntityFilter.Name = "labelEntityFilter";
            this.labelEntityFilter.Size = new System.Drawing.Size(74, 13);
            this.labelEntityFilter.TabIndex = 5;
            this.labelEntityFilter.Text = "Filter by entity:";
            this.labelEntityFilter.Visible = false;
            // 
            // labelFilterByType
            // 
            this.labelFilterByType.AutoSize = true;
            this.labelFilterByType.Location = new System.Drawing.Point(684, 38);
            this.labelFilterByType.Name = "labelFilterByType";
            this.labelFilterByType.Size = new System.Drawing.Size(69, 13);
            this.labelFilterByType.TabIndex = 4;
            this.labelFilterByType.Text = "Filter by type:";
            this.labelFilterByType.Visible = false;
            // 
            // panelObjectFilter
            // 
            this.panelObjectFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelObjectFilter.Controls.Add(this.comboBoxTypeFilter);
            this.panelObjectFilter.Location = new System.Drawing.Point(759, 34);
            this.panelObjectFilter.Name = "panelObjectFilter";
            this.panelObjectFilter.Size = new System.Drawing.Size(125, 21);
            this.panelObjectFilter.TabIndex = 3;
            // 
            // comboBoxTypeFilter
            // 
            this.comboBoxTypeFilter.DisplayMember = "Name";
            this.comboBoxTypeFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTypeFilter.FormattingEnabled = true;
            this.comboBoxTypeFilter.Location = new System.Drawing.Point(0, 0);
            this.comboBoxTypeFilter.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxTypeFilter.Name = "comboBoxTypeFilter";
            this.comboBoxTypeFilter.Size = new System.Drawing.Size(123, 21);
            this.comboBoxTypeFilter.TabIndex = 3;
            this.comboBoxTypeFilter.ValueMember = "Value";
            this.comboBoxTypeFilter.Visible = false;
            this.comboBoxTypeFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTypeFilter_SelectedIndexChanged);
            // 
            // textBoxSearchTerm
            // 
            this.textBoxSearchTerm.Enabled = false;
            this.textBoxSearchTerm.Location = new System.Drawing.Point(55, 34);
            this.textBoxSearchTerm.Name = "textBoxSearchTerm";
            this.textBoxSearchTerm.Size = new System.Drawing.Size(273, 20);
            this.textBoxSearchTerm.TabIndex = 1;
            this.textBoxSearchTerm.TextChanged += new System.EventHandler(this.TextBoxSearchTerm_TextChanged);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(7, 37);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(44, 13);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "Search:";
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResults.AutoSize = true;
            this.groupBoxResults.Controls.Add(this.listViewResults);
            this.groupBoxResults.Location = new System.Drawing.Point(16, 129);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(919, 446);
            this.groupBoxResults.TabIndex = 6;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "Results";
            // 
            // listViewResults
            // 
            this.listViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewResults.AutoArrange = false;
            this.listViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.objectTypeColumn,
            this.nameColumn,
            this.logicalNameColumn,
            this.entityColumn,
            this.infoColumn});
            this.listViewResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listViewResults.Enabled = false;
            this.listViewResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.GridLines = true;
            this.listViewResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewResults.HideSelection = false;
            this.listViewResults.Location = new System.Drawing.Point(10, 29);
            this.listViewResults.MultiSelect = false;
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(896, 407);
            this.listViewResults.TabIndex = 0;
            this.listViewResults.TabStop = false;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            this.listViewResults.DoubleClick += new System.EventHandler(this.ListViewResults_DoubleClick);
            // 
            // objectTypeColumn
            // 
            this.objectTypeColumn.Text = "Type";
            this.objectTypeColumn.Width = 120;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 400;
            // 
            // logicalNameColumn
            // 
            this.logicalNameColumn.Text = "Logical Name";
            this.logicalNameColumn.Width = 195;
            // 
            // entityColumn
            // 
            this.entityColumn.Text = "Entity";
            this.entityColumn.Width = 150;
            // 
            // infoColumn
            // 
            this.infoColumn.Text = "Info";
            this.infoColumn.Width = 200;
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(955, 594);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.panelObjectFilter.ResumeLayout(false);
            this.groupBoxResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.TextBox textBoxSearchTerm;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.ColumnHeader objectTypeColumn;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader logicalNameColumn;
        private System.Windows.Forms.ColumnHeader entityColumn;
        private System.Windows.Forms.Panel panelObjectFilter;
        private System.Windows.Forms.ComboBox comboBoxTypeFilter;
        private System.Windows.Forms.Label labelFilterByType;
        private System.Windows.Forms.Label labelEntityFilter;
        private System.Windows.Forms.ComboBox comboBoxEntityFilter;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadData;
        private ColumnHeader infoColumn;
    }
}
