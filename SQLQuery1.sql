create database DB_TracNghiem;

CREATE TABLE CauHoi (
    ID INT PRIMARY KEY,
    NoiDung NVARCHAR(MAX)
);

CREATE TABLE CauTraLoi (
    ID INT PRIMARY KEY,
    ID_CauHoi INT FOREIGN KEY REFERENCES CauHoi(ID),
    NoiDung NVARCHAR(MAX),
    LaDapAnDung BIT
);

INSERT INTO CauHoi (ID, NoiDung)
VALUES
    (1, N'Thủ đô của Việt Nam là thành phố nào?'),
    (2, N'Người đầu tiên đặt chân lên mặt trăng là ai?');

INSERT INTO CauTraLoi (ID, ID_CauHoi, NoiDung, LaDapAnDung)
VALUES
    (1, 1, N'Hà Nội', 1), -- Đánh dấu 'Hà Nội' là đáp án đúng cho Câu hỏi 1
    (2, 1, N'Hồ Chí Minh', 0),
    (3, 1, N'Đà Nẵng', 0),
    (4, 1, N'Hải Phòng', 0),
    (5, 2, N'Neil Armstrong', 1), -- Đánh dấu 'Neil Armstrong' là đáp án đúng cho Câu hỏi 2
    (6, 2, N'Buzz Aldrin', 0),
    (7, 2, N'Yuri Gagarin', 0),
    (8, 2, N'Alan Shepard', 0);
