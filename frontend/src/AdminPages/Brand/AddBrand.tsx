import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const AddBrand: React.FC = () : JSX.Element => {
    const [brand, setBrand] = useState({ name: "", image: "" });
    const [error, setError] = useState<string | null>(null);
    const apiURL = import.meta.env.VITE_PUBLIC_URL;
    const navigate = useNavigate();

    const submitForm = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            await axios.post(`${apiURL}/Brand/create`, brand);
            navigate(`/brand/all`);
        } catch (error) {
            console.error("Error adding brand", error);
            setError("Failed to add brand");
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setBrand({ ...brand, [e.target.name]: e.target.value });
    };

    return (
        <div className="p-8 max-w-md mx-auto">
            <h1 className="text-2xl font-bold text-center mb-4">Add Brand</h1>
            {error && <p className="text-red-500 text-center">{error}</p>}
            <form onSubmit={submitForm} className="space-y-4">
                <input type="text" name="name" value={brand.name} onChange={handleChange} placeholder="Enter Name" className="w-full p-2 border rounded" />
                <input type="text" name="image" value={brand.image} onChange={handleChange} placeholder="Enter Image URL" className="w-full p-2 border rounded" />
                <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded w-full">Add Brand</button>
            </form>
        </div>
    );
};

export default AddBrand;