"use client";

import { Card, CardContent, CardFooter } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { useForm } from "@tanstack/react-form";
import { useMutation } from "@tanstack/react-query";
import { useLoading } from "@/store/loadingStore";
import { useAlertStore } from "@/store/alertStore";
import { analysisTableDataType, SocialType } from "@/types/general";
import TextField from "./fields/text-field";
import { createNewDataset } from "@/app/api-functions/post-endpoints";
import FileField from "./fields/file-field";
import React from "react";

interface CreateFormProps {
    onDatasetCreated: (dataset: analysisTableDataType) => void;
}

const CreateForm: React.FC<CreateFormProps> = ({ onDatasetCreated }) => {
    const { isLoading, setLoading } = useLoading();
    const { showAlert } = useAlertStore();

    const mutation = useMutation({
        mutationFn: async (value: { datasetName: string; users: SocialType[] }) => {
            setLoading(true);
            try {
                return await createNewDataset(value);
            } finally {
                setLoading(false);
            }
        },
        onSuccess: (data: any) => {
            console.log(data.data);
            showAlert("Success!", "Dataset created successfully.", "success");

            onDatasetCreated({
                id: data.data.id,
                name: data.data.name,
                usersCount: data.data.usersCount,
                averageFriendsPerUser: data.data.averageFriendsPerUser,
            });
        },
        onError: (error) => {
            const errorMessage = (error as any)?.response?.data?.message || "Server error";
            showAlert("Error!", errorMessage, "error");
        },
    });

    const form = useForm({
        defaultValues: {
            datasetName: "" as string,
            users: [] as SocialType[],
        },
        onSubmit: async ({ value }) => {
            mutation.mutate(value);
        },
    });

    return (
        <form
            className="bg-slate-100 p-7 rounded-md"
            onSubmit={(e) => {
                e.preventDefault();
                form.handleSubmit();
            }}
        >
            <Card>
                <CardContent className="space-y-4">
                    <h1>Social Network Dataset Import</h1>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                        <form.Field
                            name="datasetName"
                            validators={{
                                onChange: ({ value }) => {
                                    if (value.length <= 0) return "Dataset name is required";
                                },
                            }}
                            children={(field) => <TextField field={field} />}
                        />
                        <form.Field
                            name="users"
                            validators={{
                                onChange: ({ value }) => {
                                    if (value.length <= 0) return "File is required, or data file is empty";
                                },
                            }}
                            children={(field) => <FileField field={field} />}
                        />
                    </div>
                </CardContent>
                <CardFooter>
                    <Button className="w-full" disabled={isLoading}>
                        {isLoading ? "Submitting..." : "Submit"}
                    </Button>
                </CardFooter>
            </Card>
        </form>
    );
};

export default CreateForm;
