"use client";
import React, { useEffect, useState } from "react";

import { useQuery } from "@tanstack/react-query";
import AnalysisTable from "./analysis/analysis-table";
import { analysisTableDataType } from "@/types/general";
import LoadingSpinner from "./utils/loading-spinner";
import { fetchDataset } from "@/app/api-functions/get-endpoints";

interface DisplayDatasetsProps {
    newlyCreatedDataset?: analysisTableDataType;
}

const DisplayDatasets: React.FC<DisplayDatasetsProps> = ({ newlyCreatedDataset }) => {
    // react useQuery for data fetching
    const {
        data: fetchedData,
        isError,
        error,
        isLoading,
    } = useQuery({
        queryKey: ["dataset"],
        queryFn: fetchDataset,
    });

    const [datasets, setDatasets] = useState<analysisTableDataType[]>([]);

    console.log(fetchedData);
    useEffect(() => {
        setDatasets(fetchedData);
    }, [fetchedData]);

    // Add newly created dataset to the state
    useEffect(() => {
        if (newlyCreatedDataset) {
            setDatasets((prevDatasets) => [...prevDatasets, newlyCreatedDataset]);
        }
    }, [newlyCreatedDataset]);

    return (
        <>
            <LoadingSpinner isLoading={isLoading} isError={isError} error={error} />
            {datasets?.length > 0 && <AnalysisTable tableData={datasets} />}
        </>
    );
};

export default DisplayDatasets;
