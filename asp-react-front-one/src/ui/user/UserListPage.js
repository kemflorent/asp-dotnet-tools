import React from 'react';
import {
    Link
} from "react-router-dom";
import { userList, userById } from '../../repositories/user/userQuery';
import { userDelete } from '../../repositories/user/userCommand';
import * as Constants from '../../constants';
import moment from 'moment';

const UserListPage = () => {

    const [users, setUsers] = React.useState([]);
  
    React.useEffect(() => {
        (async () => {
            loadUsers();
        })();
    }, []);

    const loadUsers = async () => {
         const userL = await userList(); 
         setUsers(userL.data);
    }

    const deleteUser = async (userId, username) => {
        if(window.confirm('Voulez-vous vraiment supprimer l\'utilisateur ' + username)) {
            await userDelete(userId);
            loadUsers();
        }
    }
 
    return (
      <>
        <div className='mg-top-5'></div>
        <div className='container'>
                <h3 className='d-inline'>
                    Liste des utilisateurs
                </h3>
                <div className='d-inline'>
                    <Link to={`/users/new`}> 
                        <button type='button' className='btn btn-success float-end'>Ajouter</button>
                    </Link>
                </div>
                
            <hr/>
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th> Nom d'utilisateur </th>
                        <th> Email </th>
                        <th> Date de creation </th>
                        <th> Actions </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        users &&
                        users.map(user => 
                            <tr key={user.id}>
                                <td> {user.username} </td>
                                <td> {user.email} </td>
                                <td> { moment(user.createdAt).format("DD/MM/YYYY") } </td>
                                <td>
                                    <div className='btn-group' role='group'>
                                        <Link to={`/users/edit/${user.id}`}>
                                            <button type='button' className='btn btn-primary'>Modifier</button>
                                        </Link>
                                        <button type='button' onClick={ (e) => deleteUser(user.id, user.username) } className='btn btn-danger'>Supprimer</button>
                                    </div>
                                </td>
                            </tr>   
                        )
                    }
                </tbody>
            </table>
        </div>
      </>
    );
}

export default UserListPage;