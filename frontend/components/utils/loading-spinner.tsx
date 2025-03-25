import React from "react";
import { HashLoader } from "react-spinners";

interface LoadingSpinnerProps {
    isLoading: boolean;
    isError: boolean;
    error: Error | null;
}

const LoadingSpinner: React.FC<LoadingSpinnerProps> = ({ isLoading, isError, error }) => {
    if (isLoading)
        return (
            <div className="w-full h-full flex flex-col items-center justify-center">
                <HashLoader color="gray" speedMultiplier={1.6} loading={isLoading} />
                <p>Loading data from db ...</p>
            </div>
        );
    if (isError) return <div className="text-red-400">Error: {(error as Error).message}</div>;
};

export default LoadingSpinner;
