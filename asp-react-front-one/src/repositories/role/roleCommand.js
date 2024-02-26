import React from 'react';
import * as Constants from '../../constants';
import axios from "axios";

export const roleSave = async (role) => {
    return await axios.post(`/api/role`, role);
}

export const roleUpdate = async (role) => {
    return await axios.put(`/api/role`, role);
}

export const roleDelete = async (roleId) => {
    return await axios.delete(`/api/role/${roleId}`);           //${Constants.BASE_URL}
}