import { FormattedMessage } from '@/.umi/plugin-locale/localeExports';
import { getProduct, getProducts } from '@/services/deviceCenter/Products';
import { DownloadOutlined, PlayCircleOutlined } from '@ant-design/icons';
import type { ProDescriptionsItemProps } from '@ant-design/pro-descriptions';
import ProDescriptions from '@ant-design/pro-descriptions';
import { ProFormDateRangePicker } from '@ant-design/pro-form';
import { PageContainer } from '@ant-design/pro-layout';
import type { ProColumns } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';

import { Button, Drawer, message, Tooltip } from 'antd';
import { useState } from 'react';

export default () => {
  const [showDetail, setShowDetail] = useState<boolean>(false);
  const [currentRow, setCurrentRow] = useState<API.ProductGetResponseModel>();
  const columns: ProColumns<API.ProductGetResponseModel>[] = [
    {
      title: <FormattedMessage id="pages.products.index.table.id" />,
      dataIndex: 'id',
      valueType: 'text',
      sorter: { multiple: 1 },
      search: false,
      hideInTable: true,
    },
    {
      title: <FormattedMessage id="pages.products.index.table.name" />,
      dataIndex: 'name',
      valueType: 'text',
      sorter: { multiple: 1 },
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
    },
  ];

  return (
    <PageContainer>
      <ProTable<API.ProductGetResponseModel>
        columns={columns}
        rowKey="id"
        headerTitle={
          <Button
            type="dashed"
            icon={<PlayCircleOutlined />}
            onClick={() => {
              message.success('ddd');
            }}
          >
            ok
          </Button>
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
