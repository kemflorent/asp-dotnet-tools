import React from 'react';
import {
    Link,
    useNavigate
} from "react-router-dom";
import { userList, userById } from '../../repositories/user/userQuery';
import { userDelete } from '../../repositories/user/userCommand';
import * as Constants from '../../constants';
import moment from 'moment';

const UserCreate = () => {

    // const [users, setUsers] = React.useState([]);
    let history = useNavigate();
  
    React.useEffect(() => {
        
    }, []);

    return (
        <>
        </>
    );
}

export default UserCreate;