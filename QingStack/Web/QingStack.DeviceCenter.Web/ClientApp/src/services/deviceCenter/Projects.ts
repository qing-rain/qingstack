// @ts-ignore
/* eslint-disable */
import { request } from 'umi';

/** 此处后端没有提供注释 GET /api/Projects */
export async function get(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getParams & {
    // query
    keyword?: string;
    sorter?: string;
    pageNumber?: number;
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  return request<API.ProjectGetResponseModelPagedResponseModel>('/api/Projects', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 POST /api/Projects */
export async function post(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.postParams & {
    // header
    'x-Request-Id'?: string;
  },
  body: API.CreateProjectCommand,
  options?: { [key: string]: any },
) {
  return request<API.ProjectGetResponseModel>('/api/Projects', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    params: { ...params },
    data: body,
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 GET /api/Projects/${param0} */
export async function get_2(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.get_2Params & {
    // path
    id: number;
  },
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.ProjectGetResponseModel>(`/api/Projects/${param0}`, {
    method: 'GET',
    params: { ...queryParams },
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 PUT /api/Projects/${param0} */
export async function put(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.putParams & {
    // path
    id: number;
  },
  body: API.ProjectCreateOrUpdateRequestModel,
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.ProjectGetResponseModel>(`/api/Projects/${param0}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    params: { ...queryParams },
    data: body,
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 DELETE /api/Projects/${param0} */
export async function deleteUsingDELETE(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.deleteUsingDELETEParams & {
    // path
    id: number;
  },
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<any>(`/api/Projects/${param0}`, {
    method: 'DELETE',
    params: { ...queryParams },
    ...(options || {}),
  });
}
