"use client";
import React, { useState } from "react";
import DisplayDatasets from "../display-dataset";
import CreateForm from "../forms/create-form";
import { analysisTableDataType } from "@/types/general";

const SocialNetworkPage = () => {
    const [newlyCreatedDataset, setNewlyCreatedDataset] = useState<analysisTableDataType | null>(null);

    return (
        <>
            <div className="my-8">
                <CreateForm onDatasetCreated={(dataset) => setNewlyCreatedDataset(dataset)} />
            </div>
            <div className="my-8">
                <DisplayDatasets newlyCreatedDataset={newlyCreatedDataset ?? undefined} />
            </div>
        </>
    );
};

export default SocialNetworkPage;
