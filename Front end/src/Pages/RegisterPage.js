import { useCallback, useState } from "react";
import axios from "axios";
import { AppContext } from "../Context/App.context";
import { useNavigate } from "react-router-dom";
import React from "react";
import "./MyStyle.css";
import EmployeeCredentialFormPage from "./EmployeeCredentialFormPage";

const RegisterPage = () => {
  const navigate = useNavigate();
  const [registerobj,setRegisterobj] = useState({employeeId: '', employeeEmail: '', employeePassword: '', employeeRole: ''}); 
  const [email, setEmail] = useState("");
  const [custpassword, setPwd] = useState("");
  const role = "Employee";
  const employeeId = sessionStorage.getItem("Cid");
  const [Error, setError] = useState(false);
 // const[myText,handleText]=useState("");
  const handleEmail = (event) => {
    setEmail(event.target.value);
  };
  const handlePwd = (event) => {
    setPwd(event.target.value);
  };
  const handleSubmit = async (event) => {
    registerobj.employeeId= employeeId;
    registerobj.employeeEmail=email;
    registerobj.employeePassword=custpassword;
    registerobj.employeeRole=role;

    event.preventDefault();
    try{
        const response=await axios
            .post('https://localhost:7223/api/Register/'+employeeId,
            registerobj
            )
        
        console.log(response.data);
    }
    catch(error){
        setError(true);
    }
    navigate("/");
  }
  return (
    <div>
      <nav className="navbar navbar-expand-lg navbar-light bg-dark">
        <a className="navbar-brand" href="#">
          LMS
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon" />
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item active">
              <a className="nav-link" href="#">
                Home <span className="sr-only">(current)</span>
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="#">
                Link
              </a>
            </li>
            <li className="nav-item dropdown">
              <a
                className="nav-link dropdown-toggle"
                href="#"
                id="navbarDropdown"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                Dropdown
              </a>
              <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                <a className="dropdown-item" href="#">
                  Action
                </a>
                <a className="dropdown-item" href="#">
                  Another action
                </a>
                <div className="dropdown-divider" />
                <a className="dropdown-item" href="#">
                  Something else here
                </a>
              </div>
            </li>
            <li className="nav-item">
              <a className="nav-link disabled" href="#">
                Disabled
              </a>
            </li>
          </ul>
          <form className="form-inline my-2 my-lg-0">
            <input
              className="form-control mr-sm-2"
              type="search"
              placeholder="Search"
              aria-label="Search"
              
            />
            <button
              className="btn btn-outline-success my-2 my-sm-0"
              type="submit" 
            >
              Search
            </button>
          </form>
        </div>
      </nav>
      <br></br>
      <center>
        <h1 className="title">Welcome to Loan Management Application</h1>
        <br></br>
        <h3 className="title">Register here</h3>
        <p>
          Already Registered? &nbsp;&nbsp;
          <a href="http://localhost:3000/">Login here</a>
        </p>
        <form className="myForm" onSubmit={handleSubmit}>
          <div>
            Email:{" "}
            <input
              type="text"
              value={email}
              onChange={handleEmail}
              required="true"
            />
          </div>
          <br></br>
          <div>
            Password:{" "}
            <input
              type="password"
              value={custpassword}
              onChange={handlePwd}
              required="true"
            />
          </div>
          <br></br>
          <div>
            <a href="">
              <button type="submit" style={{ backgroundColor: "#a1eafb" }}>
                {" "}
                Register{" "}
              </button>
            </a>
          </div>
          {Error && <div>Invalid Details </div>}
        </form>
      </center>
    </div>
  );
};


export default RegisterPage;
