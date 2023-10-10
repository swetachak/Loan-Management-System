import{useCallback, useState} from "react";
import axios from 'axios';
import {AppContext} from '../Context/App.context';
import {useNavigate} from 'react-router-dom';
import React from 'react';
import './MyStyle.css';

const EmployeeCredentialFormPage = () =>{
    const[Error, setError] = useState(false);
    const navigate = useNavigate();
    const[name,setName] = useState('');
    const[designation,setDesignation] = useState('');
    const[department,setDepartment] = useState('');
    const[gender,setGender] = useState('');
    const[dob,setDob] = useState('');
    const [credentialobj,setCredentialobj] = useState({employeeName: '', designation: '', department: '', gender: '', dateOfBirth: ''}); 

    const handleName = (event) =>{
        setName(event.target.value);
    }
    const handleDesignation = (event) =>{
        setDesignation(event.target.value);
    }
    const handleDepartment = (event) =>{
        setDepartment(event.target.value);
    }
    const handleGender = (event) =>{
        setGender(event.target.value);
    }
    const handleDob = (event) =>{
        setDob(event.target.value);
    }
    
    const handleSubmit = async (event) =>{
        credentialobj.employeeName=name;
        credentialobj.designation=designation;
        credentialobj.department=department;
        credentialobj.gender=gender;
        credentialobj.dateOfBirth=dob;
        event.preventDefault();
        try{
            const response=await axios
                .post('https://localhost:7223/api/AdminCustomerDataManagement',
                credentialobj
                )
            
            console.log(response.data);
            sessionStorage.setItem("Cid", response.data.employeeId);
            
            const y = sessionStorage.getItem("Cid");
            console.log(y)
        }
        catch(error){
            setError(true);
        }
        navigate('/register')
    }

    return(
        <center>
            
            <h3 className="title">Fill up the form to register </h3><br></br>
           <form className="myForm" onSubmit={handleSubmit}>
                <div>
                    Employee Name: <input type="text" onChange={handleName} />
                </div><br></br>
                <div>
                    Employee Designation:&nbsp;&nbsp; 
                    <select onChange={handleDesignation}>
                        <option value="Select">Select</option>
                        <option value="Manager">Manager</option>
                        <option value="Executive">Executive</option>
                        <option value="SExecutive">Sr.Executive</option>
                        <option value="Clerk">Clerk</option>
                    </select>
                </div><br></br>
                <div>
                    Employee Department: &nbsp;&nbsp;
                    <select onChange={handleDepartment}>
                        <option value="Select">Select</option>
                        <option value="Finance">Finance</option>
                        <option value="HR">HR</option>
                        <option value="Sales">Sales</option>
                        <option value="Technology">Technology</option>
                    </select>
                </div><br></br>
                <div>
                    Gender: &nbsp;&nbsp;
                    <select onChange={handleGender}>
                        <option value="Select">Select</option>
                        <option value="Female">Female</option>
                        <option value="Male">Male</option>
                        <option value= "Prefer not to Say"> Prefer Not Say</option>
                    </select>
                </div><br></br>
                <div>
                    Date of Birth: &nbsp;<input type="date" onChange={handleDob} />

                </div><br></br>
                <div>
                  <button class="btn1" type="submit"> Register and Continue</button>
                </div>
               
            </form> 
        </center>
    )
      
}
export default EmployeeCredentialFormPage
