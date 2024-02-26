import React from 'react';
import {
    Link,
    useNavigate,
    useParams
} from "react-router-dom";
import { userList, userById } from '../../repositories/user/userQuery';
import { userDelete } from '../../repositories/user/userCommand';
import * as Constants from '../../constants';
import moment from 'moment';

const UserEdit = (props) => {

    // const [users, setUsers] = React.useState([]);
    let history = useNavigate();
    // const params = this.props.match.params;
    let { id } = useParams();
  
    React.useEffect(() => {
        console.log("USERID ", id);
    }, []);


    return (
        <>
        </>
    );
}

export default UserEdit;