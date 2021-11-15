import { getProduct } from '@/services/deviceCenter/Products';
import type { FormInstance } from '@ant-design/pro-form';
import { DrawerForm, ProFormDateTimePicker, ProFormText } from '@ant-design/pro-form';
import { useEffect, useRef } from 'react';

export type UpdateFormProps = {
  id: string;
  visible: boolean;
  onCancel?: () => void;
  onSubmit?: (formData: API.ProductCreateOrUpdateRequestModel) => Promise<void>;
};

const UpdateForm: React.FC<UpdateFormProps> = (props) => {
  const formRef = useRef<FormInstance>();
  useEffect(() => {
    if (props.id) {
      getProduct({ id: props.id }).then((result) => {
        formRef.current?.setFieldsValue({ ...result });
      });
    }
  }, [props.id]);
  return (
    <DrawerForm<API.ProductCreateOrUpdateRequestModel>
      title="编辑产品"
      formRef={formRef}
      visible={props.visible}
      width={500}
      onVisibleChange={(v) => {
        if (!v && props?.onCancel) {
          props.onCancel();
        }
      }}
      onFinish={props.onSubmit}
    >
      <ProFormText name="id" label="产品编号" readonly />
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
    </DrawerForm>
  );
};

export default UpdateForm;
