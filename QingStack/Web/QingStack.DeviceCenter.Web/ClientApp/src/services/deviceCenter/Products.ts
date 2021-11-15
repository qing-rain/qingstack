// @ts-ignore
/* eslint-disable */
import { request } from 'umi';

/** 此处后端没有提供注释 GET /api/Products */
export async function getProducts(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getProductsParams & {
    // query
    keyword?: string;
    sorter?: string;
    pageNumber?: number;
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  return request<API.ProductGetResponseModelPagedResponseModel>('/api/Products', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 POST /api/Products */
export async function postProduct(
  body: API.ProductCreateOrUpdateRequestModel,
  options?: { [key: string]: any },
) {
  return request<API.ProductGetResponseModel>('/api/Products', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 GET /api/Products/${param0} */
export async function getProduct(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getProductParams & {
    // path
    id: string;
  },
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.ProductGetResponseModel>(`/api/Products/${param0}`, {
    method: 'GET',
    params: { ...queryParams },
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 PUT /api/Products/${param0} */
export async function putProduct(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.putProductParams & {
    // path
    id: string;
  },
  body: API.ProductCreateOrUpdateRequestModel,
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.ProductGetResponseModel>(`/api/Products/${param0}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    params: { ...queryParams },
    data: body,
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 DELETE /api/Products/${param0} */
export async function deleteProduct(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.deleteProductParams & {
    // path
    id: string;
  },
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<any>(`/api/Products/${param0}`, {
    method: 'DELETE',
    params: { ...queryParams },
    ...(options || {}),
  });
}
