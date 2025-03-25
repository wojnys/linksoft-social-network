"use client";

import React, { useState } from "react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

interface QueryProviderProps {
    children: React.ReactNode;
}
const QueryProvider: React.FC<QueryProviderProps> = ({ children }) => {
    // const [queryClient] = useState(() => new QueryClient());
    const queryClient = new QueryClient();

    return <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>;
};

export default QueryProvider;
