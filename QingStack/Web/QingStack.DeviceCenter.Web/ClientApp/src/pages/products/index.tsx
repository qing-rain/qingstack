import { getProduct, getProducts, postProduct } from '@/services/deviceCenter/Products';
import { DownloadOutlined, PlusOutlined } from '@ant-design/icons';
import type { ProDescriptionsItemProps } from '@ant-design/pro-descriptions';
import ProDescriptions from '@ant-design/pro-descriptions';
import {
  ModalForm,
  ProFormDateRangePicker,
  ProFormDateTimePicker,
  ProFormText,
} from '@ant-design/pro-form';
import { PageContainer } from '@ant-design/pro-layout';
import type { ActionType, ProColumns } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';

import type { FormInstance } from 'antd';
import { Button, Drawer, message, Tooltip } from 'antd';
import moment from 'moment';
import { useRef, useState } from 'react';
import { FormattedMessage } from 'umi';

export default () => {
  const [showDetail, setShowDetail] = useState<boolean>(false);
  const [currentRow, setCurrentRow] = useState<API.ProductGetResponseModel>();
  const tableActionRef = useRef<ActionType>();
  const createFormRef = useRef<FormInstance>();
  const columns: ProColumns<API.ProductGetResponseModel>[] = [
    {
      title: <FormattedMessage id="pages.products.index.table.id" />,
      dataIndex: 'id',
      valueType: 'text',
      sorter: { multiple: 1 },
      search: false,
      //hideInTable: true,
    },
    {
      title: <FormattedMessage id="pages.products.index.table.name" />,
      dataIndex: 'name',
      valueType: 'text',
      sorter: { multiple: 2 },
      defaultSortOrder: 'descend',
      search: {
        transform: () => 'productName',
      },
      filters: true,
      onFilter: true,
      valueEnum: {
        all: { text: '全部', status: 'Default' },
        close: { text: '关闭', status: 'Default' },
        running: { text: '运行中', status: 'Processing' },
        online: { text: '已上线', status: 'Success' },
        error: { text: '异常', status: 'Error' },
      },
      render: (dom: any, entity: any) => {
        return (
          <a
            onClick={() => {
              setCurrentRow(entity);
              setShowDetail(true);
            }}
          >
            {dom}
          </a>
        );
      },
    },
    {
      title: <FormattedMessage id="pages.products.index.table.creationTime" />,
      dataIndex: 'creationTime',
      valueType: 'dateTime',
      renderFormItem: () => {
        return <ProFormDateRangePicker name="dateRange" />;
      },
      sorter: { multiple: 3 },
      defaultSortOrder: 'descend',
    },
  ];

  return (
    <PageContainer>
      <ProTable<API.ProductGetResponseModel>
        columns={columns}
        rowKey="id"
        actionRef={tableActionRef}
        headerTitle={
          <ModalForm<API.ProductCreateOrUpdateRequestModel>
            title="创建产品"
            formRef={createFormRef}
            trigger={
              <Button type="primary">
                <PlusOutlined />
                创建产品
              </Button>
            }
            modalProps={{
              onCancel: () => console.log('run'),
            }}
            onFinish={async (values) => {
              values.creationTime = moment(values.creationTime).toISOString();
              const result = await postProduct(values);
              if (result) {
                message.success('创建产品成功。');
                createFormRef.current?.resetFields();
                tableActionRef.current?.reload();
                return true;
              } else {
                message.error('创建产品失败。');
                return false;
              }
            }}
          >
            <ProFormText
              name="name"
              label="产品名称"
              rules={[
                {
                  type: 'string',
                  required: true,
                  min: 3,
                  max: 15,
                },
              ]}
            />
            <ProFormDateTimePicker
              name="creationTime"
              label="创建时间"
              rules={[
                {
                  required: true,
                },
              ]}
            />
          </ModalForm>
        }
        options={{ fullScreen: true, search: true }}
        toolbar={{
          actions: [
            <Tooltip title="导出">
              <Button
                type="link"
                icon={<DownloadOutlined style={{ fontSize: 20 }} onClick={() => alert('ok')} />}
              />
            </Tooltip>,
          ],
        }}
        request={async (params: any, sort: any, filter: any) => {
          const result = await getProducts({
            sorter: sort,
            pageNumber: params.current,
            ...params,
            ...filter,
          });

          return {
            data: result.items,
            total: result.totalCount,
          };
        }}
        pagination={{ showSizeChanger: true, showQuickJumper: true }}
        search={{ labelWidth: 'auto' }}
      />
      <Drawer
        width={400}
        visible={showDetail}
        onClose={() => {
          setShowDetail(false);
        }}
        closable={true}
      >
        <ProDescriptions<API.ProductGetResponseModel>
          column={1}
          title={currentRow?.name}
          request={async () => {
            if (currentRow?.id) {
              return { data: await getProduct({ id: currentRow.id }) };
            }
            return { data: currentRow || {} };
          }}
          params={{
            id: currentRow?.id,
          }}
          columns={columns as ProDescriptionsItemProps<API.ProductGetResponseModel>[]}
        />
      </Drawer>
    </PageContainer>
  );
};
