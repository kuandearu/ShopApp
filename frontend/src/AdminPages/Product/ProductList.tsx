import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom';



const ProductList : React.FC = () : JSX.Element => {

  const[products, setProducts] = useState<Product[]>([]);
  const[brands, setBrands] = useState<Brand[]>([]);
  const[categories, setCategories] = useState<Category[]>([]);
  const[searchTerm, setSearchTerm] = useState<string>("");
  const[filteredResults, setFilteredResults] = useState<Product[]>([]);
  const[loading, setLoading] = useState<boolean>(true);
  const[error, setError] = useState<string | null>(null);
  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();

  // const formatDate = (date: Date | string): string => {
  //   if (!date) return 'N/A';
  //   return new Date(date).toLocaleString();
  // };

  const handleAddButton = async () => {
    navigate(`/add-product`);
  }
  const handleUpdateButton = async (id : number) => {
    navigate(`/update-product/${id}`);
  }
  const handleDetailButton = async (id : number) => {
    navigate(`/product/${id}`);
  }
  const handleDeleteButton = async (id : number) => {
    try{
      const confirmDelete = window.confirm(`Do you want to delete product: ${id}`);
      if(confirmDelete){
        await axios.delete(`${apiURL}/Product/delete/${id}`);
        setProducts(products.filter(product => product.id !== id));
      }
    }catch(error){
      console.error("Failed to delete product", error);
      setError("Failed to delete product");
    }
  }
  useEffect(() => {
    async function fetchAll(){
      try{
        const response = await axios.get<Product[]>(`${apiURL}/Product/all`);
        const brandRes = await axios.get<Brand[]>(`${apiURL}/Brand/all`);
        const categoryRes = await axios.get<Category[]>(`${apiURL}/Category/all`);
        setProducts(response.data);
        setFilteredResults(response.data);
        console.log(response.data);
        setBrands(brandRes.data);
        setCategories(categoryRes.data);

      }catch(error){
        console.error("Fail to fetch data", error);
        setError("Fail to load products");
      }finally{
        setLoading(false);
      }

    }
    fetchAll();
  }, [apiURL])

  useEffect(() => {
    const filtered = products.filter(product => 
      product.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredResults(filtered);
  }, [searchTerm, products]);

  if (loading) {
    return <div className="text-center text-lg font-semibold">Loading...</div>;
  }

  if (error) {
    return <div className="text-center text-lg text-red-500">{error}</div>;
  }

  const fromIdToName = (id: number, items: Brand[] | Category[]) => {
    const found = items.find(item => item.id === id);
    return found ? found.name : "No ID";
  }

  return (
    <div className="p-8">
      <h1 className="text-3xl font-bold text-center mb-8">Product List</h1>
      <input 
        type="text" 
        placeholder="Search by name..." 
        className="mb-4 p-2 border border-gray-300 rounded w-full"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
      />
      <table className="min-w-full bg-white border border-gray-300 shadow-lg rounded-lg overflow-hidden">
        <thead className="bg-gray-100">
          <tr>
            <th className="py-3 px-4 border-b text-left">ID</th>
            <th className="py-3 px-4 border-b text-left">Name</th>
            <th className="py-3 px-4 border-b text-left">Price</th>
            <th className="py-3 px-4 border-b text-left">Old Price</th>
            <th className="py-3 px-4 border-b text-left">Image</th>
            {/* <th className="py-3 px-4 border-b text-left">Description</th> */}
            {/* <th className="py-3 px-4 border-b text-left">Specification</th>
            <th className="py-3 px-4 border-b text-left">Buy Turn</th> */}
            {/* <th className="py-3 px-4 border-b text-left">Quantity</th> */}
            <th className="py-3 px-4 border-b text-left">Brand</th>
            <th className="py-3 px-4 border-b text-left">Category</th>
            {/* <th className="py-3 px-4 border-b text-left">Created At</th>
            <th className="py-3 px-4 border-b text-left">Updated At</th> */}
            <th className="py-3 px-4 border-b text-left">Actions</th>
            <th className="py-3 px-4 border-b text-left"></th>
          </tr>
        </thead>
        <tbody>
          {filteredResults.length > 0 ? (
            filteredResults.map((product) => (
              <tr key={product.id} className="hover:bg-gray-50 transition-colors">
                <td className="py-3 px-4 border-b">{product.id}</td>
                <td className="py-3 px-4 border-b">{product.name}</td>
                <td className="py-3 px-4 border-b">${product.price.toFixed(2)}</td>
                <td className="py-3 px-4 border-b">${product.oldPrice.toFixed(2)}</td>
                <td className="py-3 px-4 border-b">
                  <img
                    src={product.image}
                    alt={`Product: ${product.name}`}
                    className="w-12 h-12 object-cover rounded"
                  />
                </td>
                {/* <td className="py-3 px-4 border-b">{product.description}</td> */}
                {/* <td className="py-3 px-4 border-b">{product.specification}</td> */}
                {/* <td className="py-3 px-4 border-b">{product.buyTurn}</td> */}
                {/* <td className="py-3 px-4 border-b">{product.quantity}</td> */}
                <td className="py-3 px-4 border-b">{fromIdToName(product.brandId, brands)}</td>
                <td className="py-3 px-4 border-b">{fromIdToName(product.categoryId, categories)}</td>
                {/* <td className="py-3 px-4 border-b">{formatDate(product.createdAt)}</td>
                <td className="py-3 px-4 border-b">{formatDate(product.updatedAt)}</td> */}
                <td>
                  <button onClick={() => handleUpdateButton(product.id)} className="bg-blue-500 text-white px-2 py-1 rounded mr-2 cursor-pointer">Update</button>
                  <button onClick={() => handleDeleteButton(product.id)} className="bg-red-500 text-white px-2 py-1 rounded mr-2 cursor-pointer">Delete</button>
                </td>
                <td>
                <button onClick={() => handleDetailButton(product.id)} className="bg-green-500 text-white px-2 py-1 rounded mr-2 cursor-pointer">View Detail</button>
                  
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={13} className="py-6 text-center text-gray-500">
                There are no products available.
              </td>
            </tr>
          )}
        </tbody>
      </table>
      <button onClick={handleAddButton} className='bg-green-500 text-white px-2 py-2 rounded mr-2 cursor-pointer'>Add Product</button>
    </div>
  );
}

export default ProductList