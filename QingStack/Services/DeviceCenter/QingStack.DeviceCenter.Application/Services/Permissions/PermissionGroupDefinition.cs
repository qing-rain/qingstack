/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionGroupDefinition.cs
    文件功能描述：权限组定义


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionGroupDefinition
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; } = null!;
        /// <summary>
        /// 分组显示名称
        /// </summary>
        public string? DisplayName { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>

        private readonly List<PermissionDefinition> _permissions = new();

        public IReadOnlyList<PermissionDefinition> Permissions => _permissions.ToImmutableList();

        protected internal PermissionGroupDefinition([NotNull] string name, string? displayName = null)
        {
            Name = name;
            DisplayName = displayName;
        }
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <param name="displayName">权限显示名称</param>
        /// <param name="isEnabled">是否允许</param>
        /// <returns></returns>
        public virtual PermissionDefinition AddPermission([NotNull] string name, string? displayName = null, bool isEnabled = true)
        {
            var permission = new PermissionDefinition(name, displayName, isEnabled);
            _permissions.Add(permission);
            return permission;
        }
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>

        public virtual List<PermissionDefinition> GetPermissionsWithChildren()
        {
            var permissions = new List<PermissionDefinition>();

            foreach (var permission in _permissions)
            {
                AddPermissionToListRecursively(permissions, permission);
            }

            return permissions;
        }

        private void AddPermissionToListRecursively(List<PermissionDefinition> permissions, PermissionDefinition permission)
        {
            permissions.Add(permission);

            foreach (var child in permission.Children)
            {
                AddPermissionToListRecursively(permissions, child);
            }
        }
        /// <summary>
        /// 根据权限名查询权限信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public PermissionDefinition GetPermissionOrNull([NotNull] string name)
        {
            return GetPermissionOrNullRecursively(Permissions, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private PermissionDefinition GetPermissionOrNullRecursively(IReadOnlyList<PermissionDefinition> permissions, string name)
        {
            foreach (var permission in permissions)
            {
                if (permission.Name == name)
                {
                    return permission;
                }

                var childPermission = GetPermissionOrNullRecursively(permission.Children, name);
                if (childPermission is not null)
                {
                    return childPermission;
                }
            }

            return null!;
        }
    }
}
