import {react} from 'react';
import{Typography } from 'antd';
import { useContext,useState,useEffect } from 'react';
import {api} from '../Services/client';


export  const AdminPage=()=>{

    const [adminInfo, setAdminInfo] = useState<any>(null);

  useEffect(() => {
    const fetchAdminInfo = async () => {
      try {
        const res = await api.get('/profile/admin-data');
        setAdminInfo(res.data);
      } catch (err) {
        setAdminInfo(err);
        console.error(err);
      }
    };
    fetchAdminInfo();
  }, []);

    return (
        <div style={{ padding: 20 }}>
          <h2 >This is admin page</h2>
            <div>
             
             <p>{adminInfo ? adminInfo.message: ""}</p>
             <p>{adminInfo ? adminInfo.data : ""}</p>
            </div>
        </div>
    );
};