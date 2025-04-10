import axios from 'axios';
import React, { useEffect, useState } from 'react'


const RegisteredUserList : React.FC = () : JSX.Element => {
  const[registerUser, setRegisterUser] = useState<RegisteredUser[]>([]);
  const[loading, setLoading] = useState<boolean>(true);
  const[error, setError] = useState<string>("");
  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const handleAddButton = () => {

  }
  const handleUpdateButton = (id: number) => {
    
  }
  const handleDeleteButton = (id : number) => {
    
  }
  useEffect(() => {
    const fetchData = async () => {
      const response = axios.get(`${apiURL}/Auth/all`);

    }
  },[apiURL])
  return (
    <div>RegisteredUserList</div>
  )
}

export default RegisteredUserList