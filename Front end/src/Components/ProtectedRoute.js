import React, {useContext} from 'react'
import {AppContext} from '../Context/App.context'
import {Navigate} from 'react-router-dom';

const ProtectedRoute = ({ children }) => {
    const {user} =useContext(AppContext);
    if(user===null){
        return <Navigate to= "/" replace />
    }
    return children;
}
export default ProtectedRoute
