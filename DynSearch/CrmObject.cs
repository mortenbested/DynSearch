using System;
using System.Collections.Generic;

namespace DynSearch
{
    internal abstract class CrmObject
    {
        public Guid Id { get; protected set; }
        public string DisplayName { get; protected set; }
        public string SearchString;
        public string EntityDisplayName { get; protected set; }
    }

    internal class CrmSecurityRole : CrmObject
    {
        public CrmSecurityRole(Guid id, string roleName)
        {
            Id = id;
            DisplayName = roleName;
            SearchString = roleName.ToLower();
        }
    }

    internal class CrmSolution : CrmObject
    {
        public string UniqueName { get; }

        public CrmSolution(Guid id, string displayName, string uniqueName)
        {
            Id = id;
            DisplayName = displayName;
            UniqueName = uniqueName;
            SearchString = $"{displayName}{uniqueName}".ToLower();
        }
    }

    internal class CrmWorkflow : CrmObject
    {
        public string Mode { get; }
        public List<string> Triggers { get; } = new List<string>();

        public CrmWorkflow(Guid id, string name, string primaryEntityName, string mode)
        {
            Id = id;
            Mode = mode;
            DisplayName = name;
            EntityDisplayName = primaryEntityName;
            SearchString = this.DisplayName.ToLower();
        }
    }

    internal class CrmWebResource : CrmObject
    {
        public string LogicalName { get; }
        public string ResourceType { get; }

        public CrmWebResource(Guid id, string displayName, string name, string resourceType)
        {
            Id = id;
            DisplayName = displayName;
            LogicalName = name;
            ResourceType = resourceType;
            SearchString = $"{name}{displayName}".ToLower();
        }
    }

    internal class CrmPluginStep : CrmObject
    {
        public string Stage { get; }
        public string MessageType { get; }
        public string EventHandler { get; }
        public string Mode { get; }

        public CrmPluginStep(Guid id, string name, string stage, string messageType, string eventHandler, string primaryEntityName, string mode)
        {
            Id = id;
            DisplayName = name;
            Stage = stage;
            MessageType = messageType;
            EventHandler = eventHandler;
            Mode = mode;
            EntityDisplayName = primaryEntityName;
            SearchString = $"{name}{eventHandler}".ToLower();
        }
    }

    internal class CrmBusinessRule : CrmObject
    {
        public CrmBusinessRule(Guid id, string name, string primaryEntityName)
        {
            Id = id;
            DisplayName = name;
            EntityDisplayName = primaryEntityName;
            SearchString = this.DisplayName.ToLower();
        }
    }

    internal class CrmAction : CrmObject
    {
        public string UniqueName { get; }

        public CrmAction(Guid id, string displayName, string primaryEntityName, string uniqueName)
        {
            Id = id;
            DisplayName = displayName;
            EntityDisplayName = primaryEntityName;
            UniqueName = uniqueName;
            SearchString = $"{displayName}{uniqueName}".ToLower();
        }
    }

    internal class CrmEntity : CrmObject
    {
        public string LogicalName { get; }

        public CrmEntity(Guid id, string logicalName, string displayName)
        {
            Id = id;
            LogicalName = logicalName;
            DisplayName = displayName;
            EntityDisplayName = displayName;
            SearchString = $"{logicalName}{displayName}".ToLower();
        }
    }

    internal class CrmAttribute : CrmObject
    {
        public Guid EntityId { get; }
        public string LogicalName { get; }
        public string AttributeType { get; }

        public CrmAttribute(Guid id, string attributeLogicalName, string attributeDisplayName, string attributeType, Guid entityId, string entityDisplayName)
        {
            Id = id;
            EntityId = entityId;
            EntityDisplayName = entityDisplayName;
            LogicalName = attributeLogicalName;
            AttributeType = attributeType;
            DisplayName = attributeDisplayName;
            SearchString = $"{attributeLogicalName}{attributeDisplayName}".ToLower();
        }
    }
}
