import { request } from 'umi';
import { userManager } from './AuthorizeService'
import { get as getAppConfigs } from '@/services/deviceCenter/Configurations'
/** 此处后端没有提供注释 GET /api/notices */
export async function getNotices(options?: Record<string, any>) {
  return request<API.NoticeIconList>('/api/notices', {
    method: 'GET',
    ...(options || {}),
  });
}

export async function getAccessToken() {
  return (await userManager.getUser())?.access_token;
}

export async function getConfigurations() {
  return new Promise<API.ApplicationConfiguration>(async (resolve, reject) => {
    const appConfigs = await getAppConfigs();
    if (appConfigs) {
      resolve(appConfigs);
    }
    else {
      reject('appConfig is null');
    }
  });
}