import { request } from 'umi';
import { userManager } from './AuthorizeService'

/** 此处后端没有提供注释 GET /api/notices */
export async function getNotices(options?: { [key: string]: any }) {
  return request<API.NoticeIconList>('/api/notices', {
    method: 'GET',
    ...(options || {}),
  });
}

export async function getAccessToken() {
  return (await userManager.getUser())?.access_token;
}