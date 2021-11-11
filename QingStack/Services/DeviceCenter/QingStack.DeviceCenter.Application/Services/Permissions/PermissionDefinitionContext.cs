/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionDefinitionContext.cs
    文件功能描述：权限定义上下文


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionDefinitionContext
    {
        public IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 所有权限组
        /// </summary>
        internal Dictionary<string, PermissionGroupDefinition> Groups { get; }

        internal PermissionDefinitionContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Groups = new();
        }

        public virtual PermissionGroupDefinition AddGroup(string name, string? displayName = null)
        {
            if (Groups.ContainsKey(name))
            {
                throw new InvalidOperationException($"There is already an existing permission group with name: {name}");
            }

            return Groups[name] = new(name, displayName);
        }

        public virtual PermissionGroupDefinition GetGroup([NotNull] string name)
        {
            PermissionGroupDefinition? group = GetGroupOrNull(name);

            if (group is null)
            {
                throw new InvalidOperationException($"Could not find a permission definition group with the given name: {name}");
            }

            return group;
        }

        public virtual PermissionGroupDefinition? GetGroupOrNull([NotNull] string name)
        {
            if (!Groups.ContainsKey(name))
            {
                return null;
            }

            return Groups[name];
        }

        public virtual void RemoveGroup([NotNull] string name)
        {
            if (!Groups.ContainsKey(name))
            {
                throw new InvalidOperationException($"Not found permission group with name: {name}");
            }

            Groups.Remove(name);
        }

        public virtual PermissionDefinition? GetPermissionOrNull([NotNull] string name)
        {
            foreach (var groupDefinition in Groups.Values)
            {
                var permissionDefinition = groupDefinition.GetPermissionOrNull(name);

                if (permissionDefinition is not null)
                {
                    return permissionDefinition;
                }
            }

            return null;
        }
    }
}
