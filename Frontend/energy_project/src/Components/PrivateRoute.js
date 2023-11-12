import React from 'react';
import { Route, Navigate, useNavigate, Outlet } from 'react-router-dom';

const PrivateRoute = ({ element, allowedRoles, ...rest }) => {
  const jwtToken = localStorage.getItem('jwtToken');
  const currentUserRole = localStorage.getItem('currentUserRole');
  const currentUserId = localStorage.getItem('currentUserId');
  //const navigate = useNavigate();
  console.log('jwtToken:', jwtToken);
  console.log('currentUserRole:', currentUserRole);
  console.log('currentUserId:', currentUserId);
  
  // Extract userId from the URL
  const userIdFromUrl = rest.params?.userId;

  // Check if the user is authenticated, has the required role, and the userId matches
  const isAuthenticated = jwtToken && currentUserRole && jwtToken !== 'undefined' && currentUserRole !== 'undefined';
  const hasRequiredRole = allowedRoles.includes(currentUserRole);
  const isUserIdMatch = userIdFromUrl ? currentUserId === userIdFromUrl : true;

  if (isAuthenticated && hasRequiredRole && isUserIdMatch) {
    return <Outlet />;
  } else {
    // Redirect to the login page if the user doesn't meet the criteria and is not already on the login page
    <Navigate to="/login" />;
   }
};

export default PrivateRoute;
