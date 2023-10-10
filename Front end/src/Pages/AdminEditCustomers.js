import React, { useContext,useState, useEffect } from 'react';
import axios from 'axios';
import { AppContext} from '../Context/App.context';
import './MyStyle.css';
import Button from '@mui/material/Button';

const AdminEditCustomers = () => {
    const [deleteCustId, setDeleteCustId] = useState('');
    const[Error, setError] = useState(false);
    const [allCustData, setAllCustData] = useState([]);
    const [ename,SetName]=useState("");
    const [id,SetId]=useState("");
    const [dept,SetDept]=useState("");
    const [gender,SetGender]=useState("");
    const [Designation,setDesignation]=useState("");
    const [dob,SetDob]=useState("");
    const [isFormOpen, setIsFormOpen] = useState(false);
    //const [doj,SetDoj]=useState("");
    const [customerEditObj,setcustomerEditObj] = useState({employeeId: '',employeeName: '', designation: '', department: '', gender: '', dateOfBirth: ''});
    const [newCustObj, setnewCustObj]= useState({employeeName: '', designation: '', department: '', gender: '', dateOfBirth: ''})
    const handleSubmitEditCustomers = async(event)=>{
        customerEditObj.employeeId = id;
        customerEditObj.employeeName = ename;
        customerEditObj.designation = Designation;
        customerEditObj.department=dept;
        customerEditObj.dateOfBirth = dob;
        //customerEditObj.doj=doj;
        customerEditObj.gender=gender;
        console.log(customerEditObj);
        event.preventDefault();
        try{
        const response=await axios.put('https://localhost:7223/api/AdminCustomerDataManagement?EmployeeID='+id,customerEditObj);
        //alert(response.data);
        }
        catch(error){
            console.error('Error: ',error);
        }
        setIsFormOpen(false);
    }
    const handleId = (event) =>{
        SetId(event.target.value);
    }
    const handleEname = (event)=>{
        SetName(event.target.value);
    };

    const handleDob = (event)=>{
        SetDob(event.target.value);
    };

    const handleDept = (event)=>{
        SetDept(event.target.value);
    };

    const handleGender = (event)=>{
        SetGender(event.target.value);
    };

    const handleDesignation =(event) =>{
        setDesignation(event.target.value);
    };
    const handleEditCust = async(customerId)=>{
        try {
            // Find the customer record based on the customerId
            const customerToEdit = allCustData.find((customer) => customer.employeeId === customerId);
            
            // Set the state to populate the form with customer details
            setcustomerEditObj({
              employeeId: customerToEdit.employeeId,
              employeeName: customerToEdit.employeeName,
              designation: customerToEdit.designation,
              department: customerToEdit.department,
              gender: customerToEdit.gender,
              dateOfBirth: customerToEdit.dateOfBirth,
            });
            console.log(customerToEdit);
            SetId(customerToEdit.employeeId);
        setIsFormOpen(true);
        }
        catch (error) {
            console.error('Error editing customer:', error);
          }
        
    }

    const handleNewCustomer = async(event)=>{
        newCustObj.employeeName=ename;
        newCustObj.designation=Designation;
        newCustObj.department=dept;
        newCustObj.gender=gender;
        newCustObj.dateOfBirth=dob;
        event.preventDefault();

        try{
            console.log(newCustObj);
            const response=await axios
                .post('https://localhost:7223/api/AdminCustomerDataManagement',
                newCustObj
                )
            
            console.log(response.data);
        }
        catch(error){
            setError(true);
        }
    }

    const handleGetAllCustomers = () => {
        axios
            .get('https://localhost:7223/api/AdminCustomerDataManagement')
            .then((result) => setAllCustData(result.data));
        console.log(allCustData);
    }

    const handleDeleteCustId =(event) => {
        setDeleteCustId(event.target.value);
        console.log(deleteCustId);
    }
    const handleDeleteCust = async (customerId) => {
        try {
          // Implement your delete logic here based on the customerId
          // You can use this function to trigger the delete action
          const response = await axios.delete(`https://localhost:7223/api/AdminCustomerDataManagement?EmployeeID=${customerId}`);
          console.log(response.data);
    
          // Refresh the customer list after deletion
          handleGetAllCustomers();
        } catch (error) {
          console.error('Error deleting customer:', error);
        }
      };
    
    return(
        <center><br></br>
           <h1 className="title">Loan Management Application</h1> <br></br>
           <h3 className="cl1">Customer Master Data Details</h3><br></br><br></br>
           <form className="myForm" onSubmit={handleNewCustomer}>
           <div>
                    Employee Name: &nbsp; <input type="text" value={ename} onChange={handleEname}/> 
                </div><br></br>
                <div>
                Date of Birth:&nbsp;<input type="date" value={dob} onChange={handleDob}/>
                &nbsp;&nbsp;&nbsp;
                    Designation : &nbsp;<select value={Designation} onChange={handleDesignation}>
                                            <option value="select">Select</option>
                                            <option value="Manager">Manager</option>
                                            <option value="Executive">Executive</option>
                                            <option value="Sr.Executive">Sr.Executive</option>
                                            <option value="Clerk">Clerk</option>
                                        </select>
                </div><br></br>

                <div>
                    Department:&nbsp;<select value={dept} onChange={handleDept}>
                                            <option value="select">Select</option>
                                            <option value="Finance">Finance</option>
                                            <option value="HR">HR</option>
                                            <option value="Sales">Sales</option>
                                            <option value="Technology">Technology</option>
                                        </select>&nbsp;&nbsp;
                                        
                </div><br></br>
                <div>
                    Gender : &nbsp;<select value={gender} onChange={handleGender}>
                                            <option value="Male">Male</option>
                                            <option value="Female">Female</option>
                                    </select>
                </div><br></br>
                <div>
                    <p><button className="btn1" type="submit">Add Data</button></p>&nbsp;&nbsp;
                 </div>   
           </form>
           <button className='btn1' onClick={handleGetAllCustomers}>Get All Customers</button> &nbsp;&nbsp;
           {allCustData.map((allCustData, index) => (
                    <div key={index}>
                        <div className="card-body">Employee ID: {allCustData?.employeeId}</div>
                        <div className="card-body">Employee name: {allCustData?.employeeName}</div>
                        <div className="card-body">Designation: {allCustData?.designation}</div>
                        <div className="card-body">Department: {allCustData?.department}</div>
                        <div className="card-body">Gender: {allCustData?.gender}</div>
                        <div className="card-body">Date of Birth: {allCustData?.dateOfBirth}</div>
                        <div className="card-body">Date of Joining {allCustData?.dateOfJoining}</div>
                        <div> <Button className="btn1" onClick={() => handleEditCust(allCustData?.employeeId)}>Edit Customers</Button> 
                        {isFormOpen &&(
                            <form onSubmit={handleSubmitEditCustomers}>
                                <div>
                                    Employee ID: <input type="text" value={id} readOnly />
                                </div>
                                <div>
                    Employee Name: &nbsp; <input type="text" value={ename} onChange={handleEname}/> 
                </div><br></br>
                <div>
                Date of Birth:&nbsp;<input type="date" value={dob} onChange={handleDob}/>
                &nbsp;&nbsp;&nbsp;
                    Designation : &nbsp;<select value={Designation} onChange={handleDesignation}>
                                            <option value="select">Select</option>
                                            <option value="Manager">Manager</option>
                                            <option value="Executive">Executive</option>
                                            <option value="Sr.Executive">Sr.Executive</option>
                                            <option value="Clerk">Clerk</option>
                                        </select>
                </div><br></br>

                <div>
                    Department:&nbsp;<select value={dept} onChange={handleDept}>
                                            <option value="select">Select</option>
                                            <option value="Finance">Finance</option>
                                            <option value="HR">HR</option>
                                            <option value="Sales">Sales</option>
                                            <option value="Technology">Technology</option>
                                        </select>&nbsp;&nbsp;
                                        
                </div><br></br>
                <div>
                    Gender : &nbsp;<select value={gender} onChange={handleGender}>
                                            <option value="Male">Male</option>
                                            <option value="Female">Female</option>
                                    </select>
                </div>
                                <div>
                                    <button type = "submit"> Edit Details </button>
                                </div>
                            </form>

                        )}  
                            
                        </div>

                        <div>
            <Button className="btn1" onClick={() => handleDeleteCust(allCustData?.employeeId)}>
              Delete Customer
            </Button>
          </div>
                        <br></br>
                    </div>
                ))}
            <button className='btn1'>Log Out</button>
        </center>
    )
}

export default AdminEditCustomers;