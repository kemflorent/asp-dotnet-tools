import React from 'react';
import {
    Link
} from "react-router-dom";
import { roleList, roleById } from '../../repositories/role/roleQuery';
import moment from 'moment';

const RoleListPage = () => {

    const [roles, setRoles] = React.useState([]);
  
    React.useEffect(() => {
        (async () => {
            loadRoles();
        })();
    }, []);

    const loadRoles = async () => {
        const roleL = await roleList();
        setRoles(roleL.data);
    }
 
    return (
      <div>
        <div>
            <h1>LISTE DES ROLES</h1>
            <button>Ajouter</button>
        </div>
        <hr/>
        <table className='table table-striped'>
            <thead>
                <tr>
                    <th> Code </th>
                    <th> Libelle </th>
                    <th> Date de creation </th>
                    <th> Actions </th>
                </tr>
            </thead>
            <tbody>
                {
                    roles &&
                    roles.map(role =>
                        <tr key={role.id}>
                            <td> {role.code} </td>
                            <td> {role.libelle} </td>
                            <td> { moment(role.createdAt).format("DD/MM/YYYY") } </td>
                            <td>
                                <select>
                                    <option>Editer</option>
                                    <option>Supprimer</option>
                                </select>
                            </td>
                        </tr>
                    )
                }
            </tbody>
        </table>
      </div>
    );
}

export default RoleListPage;