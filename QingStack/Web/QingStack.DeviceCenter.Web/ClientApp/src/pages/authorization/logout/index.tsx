import React, { useEffect } from 'react';
import { useModel } from 'umi';
import { userManager } from '../services/AuthorizeService';
import { PageLoading } from '@ant-design/pro-layout';


const UserLogout: React.FC = () => {

  const { initialState, setInitialState } = useModel('@@initialState');
  useEffect(() => {
    async function fetchRedirect() {
      setInitialState({ ...initialState, currentUser: undefined });
      const params = new URLSearchParams(window.location.search);
      const returnUrl = params.get('returnUrl') || window.location.origin;
      userManager.signoutRedirect({ state: returnUrl });
    }
    fetchRedirect();
  }, []);

  return <PageLoading tip='Loading...' />;

};

export default UserLogout;