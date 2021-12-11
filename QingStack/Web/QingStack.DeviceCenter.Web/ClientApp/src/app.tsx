import type { Settings as LayoutSettings } from '@ant-design/pro-layout';
import { PageLoading } from '@ant-design/pro-layout';
import type { RunTimeLayoutConfig } from 'umi';
import { FormattedMessage } from 'umi';
import { history } from 'umi';
import RightContent from '@/components/RightContent';
import Footer from '@/components/Footer';
import type { User } from 'oidc-client';
import { userManager } from './pages/authorization/services/AuthorizeService';

const isDev = process.env.NODE_ENV === 'development';

/** 获取用户信息比较慢的时候会展示一个 loading */
export const initialStateConfig = {
  loading: <PageLoading />,
};

/**
 * @see  https://umijs.org/zh-CN/plugins/plugin-initial-state
 * */
export async function getInitialState(): Promise<{
  settings?: Partial<LayoutSettings>;
  currentUser?: User;
}> {
  return {
    currentUser: (await userManager.getUser()) ?? undefined,
    settings: {},
  };
}

// ProLayout 支持的api https://procomponents.ant.design/components/layout
export const layout: RunTimeLayoutConfig = ({ initialState }) => {
  return {
    headerTitleRender: (logo) => (
      <a>
        {logo}
        <h1>
          <FormattedMessage id="site.title" />
        </h1>
      </a>
    ),
    rightContentRender: () => <RightContent />,
    disableContentMargin: false,
    waterMarkProps: {
      //content: initialState?.currentUser?.name,
    },
    footerRender: () => <Footer />,
    onPageChange: () => {
      const { location } = history;
      // 如果没有登录，重定向到 login
      if (!initialState?.currentUser && !location.pathname.startsWith('/authorization')) {
        history.push(`/authorization/login?returnUrl=${encodeURIComponent(window.location.href)}`);
      }
    },
    links: isDev ? [] : [],
    menuHeaderRender: undefined,
    // 自定义 403 页面
    // unAccessible: <div>unAccessible</div>,
    ...initialState?.settings,
  };
};
