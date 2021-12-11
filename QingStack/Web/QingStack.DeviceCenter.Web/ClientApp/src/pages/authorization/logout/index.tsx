import React, { useEffect } from 'react';
import { useModel } from 'umi';
import { userManager } from '../services/AuthorizeService';
import { PageLoading } from '@ant-design/pro-layout';
import { getConfigurations } from '../services/user-service';

const UserLogout: React.FC = () => {
  const { initialState, setInitialState } = useModel('@@initialState');
  useEffect(() => {
    async function fetchRedirect() {
      const appConfigs = await getConfigurations();
      setInitialState({ ...initialState, currentUser: undefined, appConfigs: appConfigs });
      const params = new URLSearchParams(window.location.search);
      const returnUrl = params.get('returnUrl') || window.location.origin;
      userManager.signoutRedirect({ state: returnUrl });
    }
    fetchRedirect();
  }, [initialState, setInitialState]);

  return <PageLoading tip="Loading..." />;
};

export default UserLogout;
