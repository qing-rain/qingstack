declare namespace API {
  type CreateProjectCommand = {
    name?: string;
    creationTime?: string;
  };

  type PermissionGrantModel = {
    name?: string;
    displayName?: string;
    parentName?: string;
    isGranted?: boolean;
    allowedProviders?: string[];
  };

  type PermissionGroupModel = {
    name?: string;
    displayName?: string;
    permissions?: PermissionGrantModel[];
  };

  type PermissionListResponseModel = {
    entityDisplayName?: string;
    groups?: PermissionGroupModel[];
  };

  type PermissionUpdateRequestModel = {
    name?: string;
    isGranted?: boolean;
  };

  type ProductCreateOrUpdateRequestModel = {
    id?: string;
    name?: string;
    creationTime?: string;
  };

  type ProductGetResponseModel = {
    id?: string;
    name?: string;
    creationTime?: string;
  };

  type ProductGetResponseModelPagedResponseModel = {
    items?: ProductGetResponseModel[];
    totalCount?: number;
  };

  type ProjectCreateOrUpdateRequestModel = {
    id?: number;
    name?: string;
    creationTime?: string;
  };

  type ProjectGetResponseModel = {
    id?: number;
    name?: string;
    creationTime?: string;
  };

  type ProjectGetResponseModelPagedResponseModel = {
    items?: ProjectGetResponseModel[];
    totalCount?: number;
  };

  type getParams = {
    providerName?: string;
    providerKey?: string;
  };

  type updateParams = {
    providerName?: string;
    providerKey?: string;
  };

  type getProductsParams = {
    sorting?: string;
    pageNumber?: number;
    pageSize?: number;
  };

  type getParams = {
    id: string;
  };

  type putParams = {
    id: string;
  };

  type deleteParams = {
    id: string;
  };

  type getParams = {
    keyword?: string;
    sorting?: string;
    pageNumber?: number;
    pageSize?: number;
  };

  type postParams = {
    'x-Request-Id'?: string;
  };

  type getParams = {
    id: number;
  };

  type putParams = {
    id: number;
  };

  type deleteParams = {
    id: number;
  };
}
