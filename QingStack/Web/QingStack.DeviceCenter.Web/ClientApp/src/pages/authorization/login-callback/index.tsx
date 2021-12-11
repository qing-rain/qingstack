import { useModel } from 'umi';
import React, { useEffect } from 'react';
import { userManager } from '../services/AuthorizeService';
import { PageLoading } from '@ant-design/pro-layout';

const UserLoginCallback: React.FC = () => {
  const { initialState, setInitialState } = useModel('@@initialState');
  useEffect(() => {
    async function fetchRedirect() {
      const user = await userManager.signinRedirectCallback();
      if (user) {
        setInitialState({ ...initialState, currentUser: user, });
        window.location.replace(user.state);
      }
    }
    fetchRedirect();
  }, []);

  return <PageLoading tip='Loading...' />;
};

export default UserLoginCallback;