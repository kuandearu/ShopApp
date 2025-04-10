import React, { useState } from "react";

interface SearchProps<T> {
  data: T[];
  searchFields: (keyof T)[];
  onSearchResults: (results: T[]) => void;
  placeholder?: string;
}

const Search = <T,>({ data, searchFields, onSearchResults, placeholder = "Search..." }: SearchProps<T>) => {
  const [query, setQuery] = useState("");

  const handleSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;
    setQuery(value);
    
    const filteredResults = data.filter((item) =>
      searchFields.some((field) => 
        String(item[field]).toLowerCase().includes(value.toLowerCase())
      )
    );
    onSearchResults(filteredResults);
  };

  return (
    <div className="w-full max-w-md mx-auto">
      <input
        type="text"
        value={query}
        onChange={handleSearch}
        placeholder={placeholder}
        className="w-full p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
      />
    </div>
  );
};

export default Search;