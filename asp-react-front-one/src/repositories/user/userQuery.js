import React from 'react';
import * as Constants from '../../constants';
import axios from "axios";

export const userList = async () => {
    return await axios.get(`/api/user`);
}

export const userById = async (userId) => {
    return await axios.get(`/api/user/${userId}`);      // ${Constants.BASE_URL}
}