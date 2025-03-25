onmessage = (ev) => {
    const grouped = Object.groupBy(ev.data, (item) => item.userId);
    console.log("worker", grouped);
    postMessage(grouped);
};
