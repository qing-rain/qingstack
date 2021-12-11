import type { User } from "oidc-client";

/**
 * @see https://umijs.org/zh-CN/plugins/plugin-access
 * */
export default function access(initialState: {
  currentUser?: User | undefined;
  appConfigs?: API.ApplicationConfiguration;
}) {
  const { appConfigs } = initialState || {};
  const policies = appConfigs?.permissions?.policies;
  const grantedPolicies = appConfigs?.permissions?.grantedPolicies;
  const permissions = {};
  for (const key in policies) {
    if (Object.prototype.hasOwnProperty.call(policies, key)) {
      permissions[key] = Object.prototype.hasOwnProperty.call(grantedPolicies, key) &&
        grantedPolicies?.[key]
    }
  }
  return {
    ...permissions,
  };
}
