import React from 'react';
import * as Constants from '../../constants';
import axios from "axios";

export const userSave = async (user) => {
    return await axios.post(`/api/user`, user);
}

export const userUpdate = async (user) => {
    return await axios.put(`/api/user`, user);
}

export const userDelete = async (userId) => {
    return await axios.delete(`/api/role/${userId}`);       //${Constants.BASE_URL}
}