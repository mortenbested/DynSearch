using System;
using System.Windows.Forms;

namespace DynSearch
{
    internal abstract class ResultItem
    {
        public CrmObject CrmObject { get; protected set; }
        public int ImageIndex { get; protected set; }
        public string Type { get; protected set; }
        public string Name { get; protected set; }
        public string LogicalName { get; protected set; }
        public string EntityDisplayName { get; protected set; }
        public string Info { get; protected set; }
        public string Url { get; protected set; }

        public static ResultItem Get(CrmObject crmObject)
        {
            if (crmObject is CrmWorkflow workflow) return new ResultItemWorkflow(workflow);
            else if (crmObject is CrmAction action) return new ResultItemAction(action);
            else if (crmObject is CrmBusinessRule businessRule) return new ResultItemBusinessRule(businessRule);
            else if (crmObject is CrmEntity entity) return new ResultItemEntity(entity);
            else if (crmObject is CrmAttribute field) return new ResultItemField(field);
            else if (crmObject is CrmWebResource webresource) return new ResultItemWebResource(webresource);
            else if (crmObject is CrmSecurityRole role) return new ResultItemRole(role);
            else if (crmObject is CrmPluginStep step) return new ResultItemPluginStep(step);
            else if (crmObject is CrmSolution solution) return new ResultItemSolution(solution);
            else
            {
                throw new Exception("Unknown type " + crmObject.GetType().ToString());
            }
        }

        public ListViewItem AsListViewItem()
        {
            var columns = new string[] { this.Type, this.Name, this.LogicalName, this.EntityDisplayName, this.Info };
            var item = new ListViewItem(columns)
            {
                Tag = this,
                ImageIndex = this.ImageIndex
            };
            return item;
        }
    }

    internal class ResultItemSolution : ResultItem
    {
        public ResultItemSolution(CrmSolution solution)
        {
            this.CrmObject = solution;
            this.ImageIndex = Icons.IconSolutionIndex;
            this.Type = "Solution";
            this.Name = solution.DisplayName;
            this.LogicalName = solution.UniqueName;
            this.Url = "tools/solution/edit.aspx?id=" + solution.Id;
        }
    }

    internal class ResultItemRole : ResultItem
    {
        public ResultItemRole(CrmSecurityRole role)
        {
            this.CrmObject = role;
            this.ImageIndex = Icons.IconRoleIndex;
            this.Type = "Security Role";
            this.Name = role.DisplayName;
            this.Url = "biz/roles/edit.aspx?id=" + role.Id;
        }
    }

    internal class ResultItemPluginStep : ResultItem
    {
        public ResultItemPluginStep(CrmPluginStep pluginStep)
        {
            this.CrmObject = pluginStep;
            this.ImageIndex = Icons.IconBusinessWebResourceIndex;
            this.Type = "Plugin Step";
            this.Name = pluginStep.DisplayName;
            this.LogicalName = pluginStep.EventHandler;
            this.EntityDisplayName = pluginStep.EntityDisplayName;
            this.Info = $"{pluginStep.MessageType} - {pluginStep.Stage} - {pluginStep.Mode}";
        }
    }

    internal class ResultItemWebResource : ResultItem
    {
        public ResultItemWebResource(CrmWebResource webresource)
        {
            this.CrmObject = webresource;
            this.ImageIndex = Icons.IconBusinessWebResourceIndex;
            this.Type = "Web Resource";
            this.Name = webresource.DisplayName;
            this.LogicalName = webresource.DisplayName;
            this.Url = "main.aspx?etc=9333&pagetype=webresourceedit&id=" + webresource.Id;
            this.Info = webresource.ResourceType;
        }
    }

    internal class ResultItemWorkflow : ResultItem
    {
        public ResultItemWorkflow(CrmWorkflow workflow)
        {
            this.CrmObject = workflow;
            this.ImageIndex = Icons.IconProcessIndex;
            this.Type = "Workflow";
            this.Name = workflow.DisplayName;
            this.EntityDisplayName = workflow.EntityDisplayName;
            this.Url = "sfa/workflow/edit.aspx?id=" + workflow.Id;
        }
    }

    internal class ResultItemAction : ResultItem
    {
        public ResultItemAction(CrmAction action)
        {
            this.CrmObject = action;
            this.ImageIndex = Icons.IconProcessIndex;
            this.Type = "Action";
            this.LogicalName = action.UniqueName;
            this.Name = action.DisplayName;
            this.EntityDisplayName = action.EntityDisplayName;
            this.Url = "sfa/workflow/edit.aspx?id=" + action.Id;
        }
    }

    internal class ResultItemBusinessRule : ResultItem
    {
        public ResultItemBusinessRule(CrmBusinessRule businessRule)
        {
            this.CrmObject = businessRule;
            this.ImageIndex = Icons.IconBusinessRuleIndex;
            this.Type = "Business Rule";
            this.Name = businessRule.DisplayName;
            this.EntityDisplayName = businessRule.EntityDisplayName;
            this.Url = "tools/systemcustomization/businessrules/businessRulesDesigner.aspx?id=" + businessRule.Id + "&BRlaunchpoint=BRGrid";
        }
    }

    internal class ResultItemEntity : ResultItem
    {
        public ResultItemEntity(CrmEntity entity)
        {
            this.CrmObject = entity;
            this.ImageIndex = Icons.IconEntityIndex;
            this.Type = "Entity";
            this.Name = entity.EntityDisplayName;
            this.LogicalName = entity.LogicalName;
            this.EntityDisplayName = entity.EntityDisplayName;
            this.Url = "tools/systemcustomization/entities/manageentity.aspx?id=" + entity.Id;
        }
    }

    internal class ResultItemField : ResultItem
    {
        public ResultItemField(CrmAttribute field)
        {
            this.CrmObject = field;
            this.ImageIndex = Icons.IconFieldIndex;
            this.Type = "Field";
            this.Name = field.DisplayName;
            this.LogicalName = field.LogicalName;
            this.EntityDisplayName = field.EntityDisplayName;
            this.Url = $"tools/systemcustomization/attributes/manageAttribute.aspx?attributeId={field.Id}&entityId={field.EntityId}";
            this.Info = field.AttributeType;
        }
    }
}
