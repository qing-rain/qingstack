/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionDefinition.cs
    文件功能描述：权限定义


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionDefinition
    {
        /// <summary>
        /// 权限名称 AA.BB.XX
        /// </summary>
        public string Name { get; } = null!;
        /// <summary>
        /// 父级权限
        /// </summary>

        public PermissionDefinition? Parent { get; private set; }
        /// <summary>
        /// 权限授予者
        /// </summary>
        public List<string> AllowedProviders { get; set; } = new();
        /// <summary>
        /// 权限显示名
        /// </summary>
        public string? DisplayName { get; set; }
        /// <summary>
        /// 子权限
        /// </summary>

        private readonly List<PermissionDefinition> _children = new();

        public IReadOnlyList<PermissionDefinition> Children => _children.ToImmutableList();
        /// <summary>
        /// 是否允许
        /// </summary>
        public bool IsEnabled { get; set; }

        protected internal PermissionDefinition([NotNull] string name, string? displayName = null, bool isEnabled = true)
        {
            Name = name;
            DisplayName = displayName;
            IsEnabled = isEnabled;
        }
        /// <summary>
        /// 增加子权限
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public virtual PermissionDefinition AddChild([NotNull] string name, string? displayName = null, bool isEnabled = true)
        {
            var child = new PermissionDefinition(name, displayName, isEnabled) { Parent = this };
            _children.Add(child);
            return child;
        }
        /// <summary>
        /// 分配权限授予
        /// </summary>
        /// <param name="providers"></param>
        /// <returns></returns>
        public virtual PermissionDefinition WithProviders(params string[] providers)
        {
            if (providers is not null && providers.Any())
            {
                AllowedProviders.AddRange(providers);
            }

            return this;
        }
    }
}
