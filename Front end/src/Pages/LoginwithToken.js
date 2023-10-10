import {useContext, useState} from "react";
import axios from 'axios';
import {AppContext} from '../Context/App.context';
import {useNavigate} from 'react-router-dom';
import React from 'react';
const LoginwithToken = () =>{
    const [loginobj,setLogin] = useState({username: '', password: ''});
    const[username,setUsername] = useState('');
    const[password,setpwd] = useState('');
    const[Error, setError] = useState(false);
    const{ user, setUser}= useContext(AppContext);
    const navigate = useNavigate();
    const handleUsername = (event) =>{
        setUsername(event.target.value);
    }
    const handlepwd = (event) =>{
        setpwd(event.target.value);
    }
    const handleSubmit = async(event)=>{
        loginobj.username=username;
        loginobj.password=password;
        event.preventDefault();
        try{
            const response=await axios
                .post('https://localhost:7223/api/Login',
                loginobj
                )
            setUser(response.data);
            console.log(response.data);
            sessionStorage.setItem("Cid", response.data.employee_Id);
            sessionStorage.setItem("role", response.data.role);
            const y = sessionStorage.getItem("Cid");
            console.log(y)
            if(response.data.role==='Admin'){
                //navigate('/customer');
                navigate('/AdminPortal')
            }
            if(response.data.role==='Employee'){
                navigate('/UserPortal');
            }
        }
        catch(error){
            setError(true);
        }
    }
    
    return (
   
        <center>
  <title>Login Page</title>
  <br></br><br></br>
  <link
    rel="stylesheet"
    href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css"
    integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO"
    crossOrigin="anonymous"
  />
  {/*Fontawesome CDN*/}
  <link
    rel="stylesheet"
    href="https://use.fontawesome.com/releases/v5.3.1/css/all.css"
    integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU"
    crossOrigin="anonymous"
  />
  {/*Custom styles*/}
  <link rel="stylesheet" type="text/css" href="styles.css" />
  <div className="container">
    <div className="d-flex justify-content-center h-100">
      <div className="card">
        <div className="card-header">
          <h3>Sign In</h3>
        </div>
        <div className="card-body">
          <form>
            <div className="input-group form-group">
              <div className="input-group-prepend">
                <span className="input-group-text">
                  <i className="fas fa-user" />
                </span>
              </div>
              <input
                type="text"
                className="form-control"
                placeholder="username" value={username} onChange={handleUsername} />
            </div>
            <div className="input-group form-group">
              <div className="input-group-prepend">
                <span className="input-group-text">
                  <i className="fas fa-key" />
                </span>
              </div>
              <input
                type="password"
                className="form-control"
                placeholder="password" value={password} onChange={handlepwd}
              />
            </div>
            <div className="row align-items-center remember">
              <input type="checkbox" />
              Remember Me
            </div>
            <div className="form-group">
              <input
                type="submit"
                defaultValue="Login"
                className="btn float-right login_btn" onClick={handleSubmit}
              />
            </div>
          </form>
        </div>
        <div className="card-footer">
          <div className="d-flex justify-content-center links">
            Don't have an account?<a href="http://localhost:3000/employeeCredentials">Sign Up</a>
          </div>
          
        </div>
      </div>
    </div>
  </div>
</center>

        
    )
}
export default LoginwithToken
