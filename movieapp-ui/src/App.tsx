import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import LayOut from './components/layout'
import { Button } from '@mui/material';
import { QueryClient, QueryClientProvider, useQuery } from "react-query";

function App() {
  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <div className="App">
        <LayOut/>
      </div>
    </QueryClientProvider>
  )
}

export default App
