export const fetchDataset = async () => {
    const res = await fetch("http://localhost:5052/api/datasets", { method: "GET" });
    return res.json();
};
