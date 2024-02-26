import React from 'react';
import * as Constants from '../../constants';
import axios from "axios";

export const roleList = async () => {
    return await axios.get(`/api/role`);
}

export const roleById = async (roleId) => {
    return await axios.get(`/api/role/${roleId}`);      // ${Constants.BASE_URL}
}