import axios from "axios";
import { SocialType } from "@/types/general";

export const createNewDataset = async (value: { datasetName: string; users: SocialType[] }) => {
    return axios.post("http://localhost:5052/api/datasets/create-dataset-with-users", value);
};
