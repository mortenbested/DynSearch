using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynSearch
{
    internal class MetadataLoader
    {
        private readonly IOrganizationService orgService;
        private readonly Action<int, string> progressChangeHandler;

        public MetadataLoader(IOrganizationService orgService, Action<int, string> progressChangeHandler)
        {
            this.orgService = orgService;
            this.progressChangeHandler = progressChangeHandler;
        }

        public List<CrmObject> LoadAll()
        {
            List<CrmObject> result = new List<CrmObject>();

            this.progressChangeHandler(10, "Loading workflows");
            result.AddRange(this.GetWorkflows());

            this.progressChangeHandler(20, "Loading plugin steps");
            result.AddRange(this.GetPluginSteps());
            
            this.progressChangeHandler(30, "Loading web resources");
            result.AddRange(this.GetWebResources());

            this.progressChangeHandler(40, "Loading roles");
            result.AddRange(this.GetSecurityRoles());

            this.progressChangeHandler(50, "Loading solutions");
            result.AddRange(this.GetSolutions());

            this.progressChangeHandler(60, "Loading entities and fields");
            result.AddRange(this.GetEntitiesAndFields());

            this.progressChangeHandler(100, "All metadata loaded");

            return result;
        }

        private IEnumerable<CrmObject> GetSolutions()
        {
            var query = new QueryExpression("solution");
            query.ColumnSet.AddColumns("isvisible", "uniquename", "friendlyname");

            var result = new List<CrmSolution>();

            var solutions = this.orgService.RetrieveMultiple(query).Entities;
            foreach (Entity solution in solutions)
            {
                result.Add(new CrmSolution(
                    solution.Id,
                    solution.GetAttributeValue<string>("friendlyname"),
                    solution.GetAttributeValue<string>("uniquename")));
            }

            return result;
        }

        private IEnumerable<CrmObject> GetSecurityRoles()
        {
            var query = new QueryExpression("role");
            query.ColumnSet.AddColumns("name");
            query.Criteria.AddCondition("parentroleid", ConditionOperator.Null);

            var result = new List<CrmObject>();

            var roles = this.orgService.RetrieveMultiple(query).Entities;
            foreach (Entity role in roles)
            {
                result.Add(new CrmSecurityRole(
                    role.Id,
                    role.GetAttributeValue<string>("name")));
            }

            return result;
        }

        private IEnumerable<CrmObject> GetWebResources()
        {
            var query = new QueryExpression("webresource");
            query.ColumnSet.AddColumns("name", "webresourcetype", "displayname");
            query.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);

            var result = new List<CrmObject>();

            var webResources = this.orgService.RetrieveMultiple(query).Entities;
            foreach (Entity webResource in webResources)
            {
                result.Add(new CrmWebResource(
                    webResource.Id,
                    webResource.GetAttributeValue<string>("displayname"),
                    webResource.GetAttributeValue<string>("name"),
                    webResource.GetFormattedValue("webresourcetype")));
            }

            return result;
        }

        private IEnumerable<CrmObject> GetPluginSteps()
        {
            var query = new QueryExpression("sdkmessageprocessingstep");
            query.ColumnSet.AddColumns("name", "stage", "eventhandler", "mode");
            query.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);
            query.Criteria.AddCondition("ishidden", ConditionOperator.Equal, false);
            var query_sdkmessagefilter = query.AddLink("sdkmessagefilter", "sdkmessagefilterid", "sdkmessagefilterid");
            query_sdkmessagefilter.Columns.AddColumns("primaryobjecttypecode", "sdkmessageid");

            var result = new List<CrmObject>();

            var pluginSteps = this.orgService.RetrieveMultiple(query).Entities;
            foreach(Entity pluginStep in pluginSteps)
            {
                result.Add(new CrmPluginStep(
                        pluginStep.Id,
                        pluginStep.GetAttributeValue<string>("name"),
                        pluginStep.GetFormattedValue("stage"),
                        pluginStep.GetFormattedValue("sdkmessagefilter1.sdkmessageid"),
                        pluginStep.GetFormattedValue("eventhandler"),
                        pluginStep.GetFormattedValue("sdkmessagefilter1.primaryobjecttypecode"),
                        pluginStep.GetFormattedValue("mode")
                        ));
            }

            return result;
        }

        private List<CrmObject> GetWorkflows()
        {
            QueryExpression query = new QueryExpression("workflow");
            query.ColumnSet.AddColumns(new string[] { "workflowid", "name", "primaryentity", "category", "uniquename", "mode" });
            query.Criteria.AddCondition("statecode", ConditionOperator.In, 0, 1);
            query.Criteria.AddCondition("type", ConditionOperator.Equal, 1);
            query.Criteria.AddCondition("category", ConditionOperator.In, 0, 2, 3);

            var workflows = this.orgService.RetrieveMultiple(query).Entities;

            var result = new List<CrmObject>();

            foreach(Entity workflow in workflows)
            {
                int category = workflow.GetAttributeValue<OptionSetValue>("category").Value;
                if (category == 0)
                {
                    result.Add(new CrmWorkflow(
                        workflow.Id,
                        workflow.GetAttributeValue<string>("name"),
                        workflow.GetFormattedValue("primaryentity"),
                        workflow.GetFormattedValue("mode")
                        ));
                }
                else if (category == 2)
                {
                    result.Add(new CrmBusinessRule(
                        workflow.Id,
                        workflow.GetAttributeValue<string>("name"),
                        workflow.GetFormattedValue("primaryentity")));
                }
                else if (category == 3)
                {
                    result.Add(new CrmAction(
                        workflow.Id,
                        workflow.GetAttributeValue<string>("name"),
                        workflow.GetFormattedValue("primaryentity"),
                    workflow.GetAttributeValue<string>("uniquename")));
                }
            }

            return result;
        }

        private List<CrmObject> GetEntitiesAndFields()
        {
            List<CrmObject> result = new List<CrmObject>();

            var entityProperties = new MetadataPropertiesExpression
            {
                AllProperties = false
            };
            entityProperties.PropertyNames.AddRange("LogicalName", "DisplayName", "Attributes");

            var request = new RetrieveMetadataChangesRequest
            {
                Query = new EntityQueryExpression
                {
                    Properties = entityProperties,
                    AttributeQuery = new AttributeQueryExpression()
                    {
                        Properties = new MetadataPropertiesExpression("DisplayName", "LogicalName", "AttributeType")
                    }
                }
            };

            RetrieveMetadataChangesResponse response = (RetrieveMetadataChangesResponse)this.orgService.Execute(request);

            foreach (EntityMetadata entityMeta in response.EntityMetadata)
            {
                string entityDisplayName = entityMeta.DisplayName.LocalizedLabels?.FirstOrDefault()?.Label;
                if (!string.IsNullOrEmpty(entityDisplayName))
                {
                    result.Add(new CrmEntity(
                    entityMeta.MetadataId.Value,
                    entityMeta.LogicalName,
                    entityDisplayName));
                }

                foreach (AttributeMetadata attributeMeta in entityMeta.Attributes)
                {
                    string attributeDisplayName = attributeMeta.DisplayName.LocalizedLabels?.FirstOrDefault()?.Label;
                    if (!string.IsNullOrEmpty(attributeDisplayName))
                    {
                        result.Add(new CrmAttribute(
                        attributeMeta.MetadataId.Value,
                        attributeMeta.LogicalName,
                        attributeDisplayName,
                        attributeMeta.AttributeType.ToString(),
                        entityMeta.MetadataId.Value,
                        entityDisplayName));
                    }
                }
            }

            return result;
        }
    }
}
