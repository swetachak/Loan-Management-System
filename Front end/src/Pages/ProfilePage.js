import React, {useContext} from 'react'
import {AppContext} from '../Context/App.context';

const ProfilePage = () =>{
    const{user,setUser} = useContext(AppContext);
    return(
        <div>

            <h1>Profile Page</h1>
            <p>{user.token}</p>
            <p>{user.user_Id}</p>
            <button onClick= {() =>{setUser(null)}}> Logout </button>
        </div>
    )
}

export default ProfilePage;