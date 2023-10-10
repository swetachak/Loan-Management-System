import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './loanrequeststyle.css';

function LoanRequestList() {
  const [loanRequests, setLoanRequests] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Fetch pending loan requests from your API
    axios
      .get('https://localhost:7223/api/AdminLoanRequestList/WaitingList')
      .then((response) => {
        setLoanRequests(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching loan requests:', error);
        setLoading(false);
      });
  }, []);

  const handleApprove = (requestId) => {
    // Send an API request to approve the loan request
    axios
      .post('https://localhost:7223/api/AdminLoanRequestList/Approve/'+requestId)
      .then((response) => {
        // If the request is successful, update the UI or show a success message
        console.log(response.data);
      })
      .catch((error) => {
        console.error('Error approving loan request:', error);
      });
  };

  const handleDecline = (requestId) => {
    // Send an API request to decline the loan request
    axios
      .post('https://localhost:7223/api/AdminLoanRequestList/Decline/'+ requestId )
      .then((response) => {
        // If the request is successful, update the UI or show a success message
        console.log(response.data);
      })
      .catch((error) => {
        console.error('Error declining loan request:', error);
      });
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h1 className="loan-request-heading">Loan Request List</h1>
      <table className="loan-request-table">
        <thead>
          <tr>
            <th>Employee Name</th>
            <th>Designation</th>
            <th>Department</th>
            <th>Gender</th>
            <th>Item Description</th>
            <th>Item Make</th>
            <th>Item Category</th>
            <th>Item Valuation</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {loanRequests.map((request) => (
            <tr key={request.requestId}>
              <td>{request.employeeName}</td>
              <td>{request.designation}</td>
              <td>{request.department}</td>
              <td>{request.gender}</td>
              <td>{request.itemDescription}</td>
              <td>{request.itemMake}</td>
              <td>{request.itemCategory}</td>
              <td>{request.itemValuation}</td>
              <td>
                <button onClick={() => handleApprove(request.requestId)}>
                  Approve
                </button>
                <button onClick={() => handleDecline(request.requestId)}>
                  Decline
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default LoanRequestList;
