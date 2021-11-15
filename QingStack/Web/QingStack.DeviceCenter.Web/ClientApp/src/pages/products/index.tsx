import { FormattedMessage } from "@/.umi/plugin-locale/localeExports";
import { getProducts } from "@/services/deviceCenter/Products"
import { PlayCircleOutlined } from "@ant-design/icons";
import type { ProColumns } from "@ant-design/pro-table";
import ProTable from "@ant-design/pro-table"
import { Button, message } from "antd";

export default () => {

    const columns: ProColumns<API.ProductGetResponseModel>[] = [
        {
            title: <FormattedMessage id='pages.products.index.table.id' />,
            dataIndex: 'id',
            valueType: 'text',
        },
        {
            title: <FormattedMessage id='pages.products.index.table.name' />,
            dataIndex: 'name',
            valueType: 'text',
        },
        {
            title: <FormattedMessage id='pages.products.index.table.creationTime' />,
            dataIndex: 'creationTime',
            valueType: 'dateTime',
        },
    ]

    return (
        <ProTable<API.ProductGetResponseModel>
            columns={columns}
            rowKey='id'
            search={false}
            headerTitle={<Button type='dashed' icon={<PlayCircleOutlined />} onClick={() => {
                message.success('ddd')
            }}>ok</Button>}
            options={{ fullScreen: true, search: true }}
            request={async (params: any) => {
                const result = await getProducts(
                    {
                        pageNumber: params.current,
                        pageSize: params.pageSize,
                    });

                return {
                    data: result.items,
                    total: result.totalCount,
                }
            }
            }
            pagination={{ showSizeChanger: true, showQuickJumper: true }}
        />
    )
}