// @ts-ignore
/* eslint-disable */
import { request } from 'umi';

/** 此处后端没有提供注释 POST /api/Validator */
export async function post(
  body: API.ProjectCreateOrUpdateRequestModel,
  options?: { [key: string]: any },
) {
  return request<API.ProjectGetResponseModel>('/api/Validator', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}
