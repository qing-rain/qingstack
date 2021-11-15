// @ts-ignore
/* eslint-disable */
import { request } from 'umi';

/** 此处后端没有提供注释 GET /api/Resources */
export async function getHelloWorld(options?: { [key: string]: any }) {
  return request<string>('/api/Resources', {
    method: 'GET',
    ...(options || {}),
  });
}
