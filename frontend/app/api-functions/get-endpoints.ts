export const fetchDataset = async () => {
    const res = await fetch("http://localhost:5052/api/datasets/with-stats", { method: "GET" });
    return res.json();
};
