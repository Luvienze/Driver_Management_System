function formatDateToISO(date) {
    if (!(date instanceof Date)) {
        date = new Date(date);
    }
    if (isNaN(date)) {
        throw new Error("Unsupported date format");
    }
    return date.toISOString().split('T')[0]; 
}

async function fetchDayOffCount(date) {
    try {
        const formattedDate = formatDateToISO(date);

        const formData = new FormData();
        formData.append("day", formattedDate);

        const response = await fetch("/Driver/GetDailyDayOffDriverCount", {
            method: "POST",
            body: formData
        });

        if (!response.ok) throw new Error(`Server error: ${response.status}`);

        const data = await response.json();
        document.getElementById("dailyDayOffBox").textContent = data.count;
    } catch (error) {
        console.error("An error has been occured during loading data:", error);
        document.getElementById("dailyDayOffBox").textContent = "Error!";
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchDayOffCount(new Date()); 
});