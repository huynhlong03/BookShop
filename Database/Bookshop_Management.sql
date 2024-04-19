create database QUANLI_SACH
use QUANLI_SACH
go

create table ROLES
(
	RoleID int identity(1,1) not null primary key,
	RoleName nvarchar(50),
	[Description] nvarchar(100)
)

create table [USER]
(
	UserID int identity(1,1) not null primary key,
	Username nvarchar(50),
	[Password] nvarchar(100),
	Fullname nvarchar(50),
	Email nvarchar(50),
	RoleID int foreign key references ROLES(RoleID)
)


create table PUBLISHER
(
	PublisherID int identity(1, 1) not null primary key,
	PublisherName nvarchar(100),
	PublisherAddress nvarchar(100),
	PublisherPhone nchar(15),
)

create table CATEGORY
(
	CategoryID int identity(1, 1) not null primary key,
	CategoryName nvarchar(50),
)


create table BOOK
(
	BookID int identity(1, 1) not null primary key,
	BookName nvarchar(100),
	Price decimal(18,0),
	Decription nvarchar(max),
	UpdateDate date,
	[Image] nvarchar(max),
	Quanlity int,
	CategoryID int,
	PublisherID int
	foreign key(CategoryID) references Category(CategoryID),
	foreign key(PublisherID) references Publisher(PublisherID)
)

create table CLIENT
(
	ClientID int identity(1,1) not null primary key,
	ClientName nvarchar(50),
	Gender nvarchar(10),
	DateOfBirth date,
	PhoneNumber nvarchar(15),
	[Address] nvarchar(200),
	Email nvarchar(100),
	UserName nvarchar(50),
	[Password] nvarchar(50)
)



create table [ORDER]
(
	OrderID int identity(1,1) not null primary key,
	OrderDate date,
	DeliveryDate date,
	Payment nvarchar(50),
	[Status] nvarchar(50),
	ClientID int,
	foreign key(ClientID) references Client(ClientID)
)

create table ORDER_DETAIL
(
	OrderID int not null,
	BookID int not null,
	Quantity int,
	Price decimal(18, 0),
	primary key(OrderID, BookID),
	foreign key(OrderID) references [Order](OrderID),
	foreign key(BookID) references Book(BookID)
)


---------------------------------- INSERT DATA -----------------------------------
-- định dạng ngày tháng năm
set dateformat dmy

--- TABLE ROLES ---
insert into ROLES
values
(N'Admin', N'Quản trị viên'),
(N'Staff', N'Nhân viên')

--- TABLE USER ---
insert into [USER]
values
(N'admin', N'123', N'Nguyễn Văn F', 'admin@gmail.com', 1)



--- TABLE PUBLISHER ---

insert into PUBLISHER
values
(N'NXB Trẻ', N'124 Nguyễn Văn Cừ Q.1 Tp.HCM', '1900156045'),
(N'NXB Thống kê', N'Biên Hòa-Đồng Nai', '1900151112'),
(N'NXB Kim đồng', N'Tp.HCM', '1900157090'),
(N'NXB Công Thương', N'số 655 Phạm Văn Đồng, Bắc Từ Liêm, Hà Nội', '02439341562'),
(N'NXB Dân trí', N'Số 9, ngõ 26, phố Hoàng Cầu, phường Ô Chợ Dừa, quận Đống Đa, Hà Nội', '02466860751'),
(N'NXB Nhân dân', N'142 Tô Hiến Thành, Q2, TP.HCM', '198237451')


--- TABLE CATEGORY ---

insert into CATEGORY
values
(N'Kinh Tế'),
(N'Khoa học Xã hội'),
(N'Văn hóa - Xã hội'),
(N'Kỹ năng sống'),
(N'Khoa học-Công nghệ'),
(N'Tiểu thuyết')

--- TABLE BOOK ---


insert into BOOK
values
(N'Essential solid mechanics', 189000, N'This Book has been developed from the lectures given by the author to the
graduate students at Institute of Mechanics - VAST - Hanoi during 2011-2019 years. Its contents are based on the author’s own reading, research, teaching experiences, and the broad literature given in the Bibliography. Chapter 1 introduces the basic concepts of solid mechanics, involving the displacement, strain, stress, energy, and deformable solid bodies. Small deformation elasticity and visco-elasticity fundamentals are discussed in the Chapter 2. Chapter 3 concerns the thin structures with respective simplifying approximation assumptions: beams, plates, and shells. Chapters 4, 5, 6 contain main research results of the author and collaborators on plasticity theory, shakedown theorems, and applications. Physical fields and their interactions are presented in Chapter 7. Fracture mechanics and related problems are considered in the Chapter 8. The book is completed with the supplements overviewing some additional specific topics.
Solid Mechanics is a large field. The selection and arrangement of the
subjects reflect the author’s own view point. Another important topics (of
Solid Mechanics) - Micro-mechanics and Homogenization, which is a major
field of the author, would be presented separately in a future Monograph.', '10-01-2020', 'edfcd3e7d5bba7e82a6f6a5d8154cb63.jpg', 2, 5, 3)

insert into BOOK
values
(N'Trí tuệ do thái', 152000, N'Trí tuệ Do Thái là một cuốn sách tư duy đầy tham vọng trong việc nâng cao khả năng tự học tập, ghi nhớ và phân tích - những điều đã khiến người Do Thái vượt trội lên, chiếm lĩnh những vị trí quan trọng trong ngành truyền thông, ngân hàng và những giải thưởng sáng tạo trên thế giới. Tuy là một cuốn sách nhỏ nhưng Trí Tuệ Do Thái lại mang trong mình tri thức về một dân tộc có thể nhỏ về số lượng nhưng vĩ đại về trí tuệ và tài năng. Cuốn sách không chỉ lý giải lý do vì sao những người Do Thái trên thế giới lại thông minh và giàu có, mà còn đặc tả con đường thành công của một người Do Thái - Jerome cùng những triết lý được đúc kết đầy giá trị. Trí Tuệ Do Thái không dừng lại ở giới hạn của một cuốn sách triết lý hay kỹ năng. Thông qua Jerome, một kẻ lông bông thích la cà, tác giả đưa người đọc vào một chuyến khám phá về trí tuệ của người Do Thái, từ đó khơi ra những giới hạn để người đọc có thể tự khai phá trí tuệ bản thân với "Năm nguyên tắc" và "Mười lăm gợi ý". Đây sẽ là những bài học quý giá dành cho những ai muốn tồn tại và phát triển mạnh mẽ, không chỉ với con đường thành công của riêng mình. Không được viết như một cuốn sách kỹ năng khô khan, Trí Tuệ Do Thái được dựng lên bằng một câu chuyện và rồi cũng khép lại với một cái kết mở, nơi những người Do Thái đang không ngừng đối mặt với cuộc sống và chinh phục nó.', '9/6/2021', '1e4eeaa44ca1893b251ad9767502ae32.jpg', 10, 4, 4)

insert into BOOK
values
(N'Tư duy ngược', 125000, N'TƯ DUY NGƯỢC Chúng ta thực sự có hạnh phúc không? Chúng ta có đang sống cuộc đời mình không? Chúng ta có dám dũng cảm chiến thắng mọi khuôn mẫu, định kiến, đi ngược đám đông để khẳng định bản sắc riêng của mình không?. Có bao giờ bạn tự hỏi như thế, rồi có câu trả lời cho chính mình? Tôi biết biết, không phải ai cũng đang sống cuộc đời của mình, không phải ai cũng dám vượt qua mọi lối mòn để sáng tạo và thành công… Dựa trên việc nghiên cứu, tìm hiểu, chắt lọc, tìm kiếm, ghi chép từ các câu chuyện trong đời sống, cũng như trải nghiệm của bản thân, tôi viết cuốn sách này. Cuốn sách sẽ giải mã bạn là ai, bạn cần tư duy ngược để thành công và hạnh phúc như thế nào và các phương pháp giúp bạn dũng cảm sống cuộc đời mà bạn muốn.', '9/6/2021', '050d67e2d58f5e291cbf25da53f55ef7.jpg', 6, 4, 5)


insert into BOOK
values
(N'Vòng xoáy đi lên', 134000, N'Trầm cảm là một vòng xoáy tiêu cực. Hẳn ai cũng biết cảm giác mắc kẹt trong một vòng xoáy tiêu cực hay vòng xoáy đi xuống là như thế nào. Vòng xoáy tiêu cực xuất hiện là do những sự kiện xảy đến với bạn và những quyết định của bạn đã làm thay đổi hoạt động trong não bộ. Nếu hoạt động trong não bộ thay đổi theo hướng tồi tệ hơn, điều này sẽ lại tiếp tục khiến mọi thứ dần vượt kiểm soát, những thứ vượt kiểm soát đến lượt chúng lại tiếp tục tác động tới não bộ theo hướng tiêu cực... Nhưng sẽ thế nào nếu cuộc đời bạn đi theo chiều hướng xoáy lên thay vì xoáy xuống? Sẽ thế nào nếu bỗng nhiên bạn trở nên sung sức hơn, ngủ ngon hơn, giao lưu với bạn bè nhiều hơn và cảm thấy hạnh phúc hơn? Thường thì chỉ cần một vài cảm xúc tích cực để khởi động quá trình này, và rồi nó sẽ thúc đẩy những thay đổi tích cực trong các lĩnh vực khác của cuộc sống – đây chính là vòng xoáy tích cực hay vòng xoáy đi lên, và hiệu quả ưu việt của nó đã được chứng minh nhiều lần, trong hàng trăm nghiên cứu khoa học. Bất kỳ thay đổi nhỏ nào cũng có thể là cú huých cần thiết cho não bộ để bắt đầu vòng xoáy đi lên. Cuốn sách này đưa ra những thay đổi cụ thể trong cuộc sống, từ đó dẫn đến những thay đổi trong hoạt động của mạch não để đảo ngược chiều hướng trầm cảm. Bước đầu tiên là rất quan trọng. Hãy vận dụng và bạn sẽ dần thấy được ích lợi của những thay đổi này nhé.', '12/4/2021', 'ef7nt3ywmrlnyb15bu5iw52mvpug0so6.jpg', 0, 4, 5)

insert into BOOK
values
(N'Trí tuệ nhân tạo và ngoại giao số: Thách thức và cơ hội (Sách tham khảo)', 239000, N'Cuốn sách gồm 14 chương, tập hợp các công trình nghiên cứu khoa học công phu của các chuyên gia trong lĩnh vực công nghệ và ngoại giao đến từ nhiều quốc gia trên thế giới, cung cấp những thông tin và góc nhìn mới mẻ về chủ đề ngoại giao số. Các chuyên gia đã vận dụng nhiều cách tiếp cận khác nhau để nêu bật các tác động qua lại giữa internet và ngoại giao. AI và ngoại giao số, đồng thời nêu lên các chủ đề mới về chương trình nghị sự ngoại giao như an ninh mạng, ngoại giao Twitter, quyền riêng tư, hay việc sử dụng các công cụ internet để thực hành ngoại giao. Bên cạnh đó, bằng cách xem xét hiện tượng ngoại giao số từ mọi góc độ, các chuyên gia không những chỉ ra các mối nguy hiểm mới từ xu hướng phát triển này, mà còn gợi ý nhiều cách thức chống lại những mối hiểm nguy đó.', '12/11/2022', '3it2d8eoj4wpgbdmnnxfjauomli61c4l.jpg', 0, 5, 2)

insert into BOOK
values
(N'KÝ SỰ BrSE - NHỮNG NẺO ĐƯỜNG NGHỀ BrSE', 168000, N'Bạn thân mến ! Đây là một quyển sách hết sức đặc biệt vì nó rất khác so với những gì các bạn đã đọc trước đây. Có thể xem nó như là một câu chuyện kể về bước đường trở thành kỹ sư cầu nối - BrSE và những lời dặn dò từ người đi trước. Lúc đọc có thể sẽ bật cười vì nhiều chỗ tụi mình (Trọng và Tuấn) viết theo lối văn … chả giống ai, nhưng nếu ngẫm kỹ sẽ thấy những trăn trở gửi gắm trong đó. Việt Nam có rất nhiều điều để tự hào, là một con dân ai cũng mang trong mình lòng yêu nước không thể đong đếm. Nhưng hãy tĩnh tâm gác niềm tự hào qua một bên để thấy rằng so với thế giới thì mình còn nhiều cái cần phấn đấu và mỗi người bắt buộc phải cố gắng từng ngày. Trước mắt để giúp bản thân thoát nghèo, sau là giúp cho người khác có công ăn việc làm. Theo mình thì đó là sự thể hiện thực tế nhất về lòng yêu nước. Quyển sách này được viết ròng rã gần 5 năm trời dựa trên kinh nghiệm hàng chục năm trong nghề kỹ sư cầu nối – là nghề sẽ giúp bạn có thu nhập cao và giải quyết được việc làm cho rất nhiều người khác. Mặc dù để trở thành BrSE giỏi không những cần thời gian dài khổ luyện mà còn cả một tính cách gan lỳ không chấp nhận bỏ cuộc. Kết quả mang lại cũng ngọt ngào tương xứng với những gì mình cố gắng. Các bạn sẽ có được mức lương thuộc top đầu trong dải lương IT không thua kém gì các Manager, cụ thể thu nhập hàng năm nếu làm ở Nhật là từ 800 triệu đến 1 tỉ 4 còn nếu ở Việt Nam thì từ 500 đến 1 tỉ. Không những thế, nghề này sẽ mở ra cho bạn nhiều cơ hội mới được làm việc và tiếp cận với tầng lớp kỹ sư của các tập đoàn hàng đầu. Nó sẽ giúp mình tiến bộ nhanh chóng và rất nhiều BrSE đã có thể tự mình đứng ra thành lập công ty riêng dựa trên kinh nghiệm cũng như các mối quan hệ trong quá trình làm công việc của một kỹ sư cầu nối. Nếu muốn theo nghề này bạn sẽ phải đánh đổi rất nhiều thời gian và công sức để vươn lên. Quan trọng là có chấp nhận đổi hay không mà thôi. Nếu đã quyết thì hãy bắt tay ngay đừng chần chừ, hướng đi đã được gói gọn trong quyển sách nhỏ này, hãy đọc và làm theo. KÝ SỰ BrSE - NHỮNG NẺO ĐƯỜNG NGHỀ BrSE (Triệu Anh Tuấn + Nguyễn Văn Trọng) Sbooks phát hành.', '26/01/2020', '120ade1b5753b010e23aad236037a91e.jpg', 4, 1, 2)

insert into BOOK
values
(N'Định Vị Bản Thân', 161000, N'Nếu nhìn từ bên ngoài, khởi nghiệp là một cái gì đó rất hoang mang và không rõ ràng. Nó hỗn loạn – tới mức không theo bất kì một kế hoạch cụ thể nào. Vì không theo kế hoạch, rất khó để tìm ra cách tốt nhất để tiếp cận Vùng đất khởi nghiệp, nhận biết khi nào thì những công việc được hoàn thành, trái ngược với việc kinh doanh theo kiểu truyền thống vô cùng bài bản và có tổ chức, với một bề dày lịch sử trải dài hàng thế kỷ của các tổ chức lâu đời cùng với những cơ hội nghề nghiệp đã được hoạch định sẵn. Định vị bản thân hướng dẫn từng bước thực tế, cung cấp phân tích của người trong cuộc về các vai trò và trách nhiệm khác nhau trong công ty khởi nghiệp - bao gồm quản lý sản phẩm, tiếp thị, tăng trưởng và bán hàng - để giúp bạn biết liệu bạn có muốn tham gia công ty khởi nghiệp không và những gì đáng mong đợi nếu bạn gia nhập. Bạn sẽ hiểu rõ hơn về cách các công ty khởi nghiệp thành công hoạt động và học cách đánh giá những người bạn có thể muốn trở thành - hoặc phấn đấu. Cuốn sách trợ giúp chúng ta: - Định vị và xác định được điểm mạnh, điểm yếu của bản thân, từ đó giúp người đọc đưa ra định hướng về nghề nghiệp, vị trí để có được lựa chọn đúng đắn trong công việc. - Phân tích cấu trúc của một công ty startup và cung cấp các kỹ năng và tư duy cần thiết ở từng bộ phận. - Hiểu rõ hơn về cách các công ty khởi nghiệp thành công hoạt động và học cách đánh giá những người bạn có thể muốn trở thành - hoặc phấn đấu. Về tác giả Jeffrey Bussgang là một nhà đầu tư mạo hiểm, doanh nhân và giáo sư kinh doanh tại Trường Kinh doanh Harvard (HBS). Hiện tại, Jeffrey đang tham gia tích cực tại quỹ đầu tư mạo hiểm Flybridge Capital Partners – Vốn hóa hơn 600 triệu đô la Mỹ. Công ty này hiện đang đầu tư và hỗ trợ quản lý cho hơn 120 doanh nghiệp, chủ yếu về lĩnh vực công nghệ. Tiêu biểu như Bowery Farming, Chief, Codecademy và MongoDB. Tại HBS, Jeffrey dạy Khởi động công nghệ mạo hiểm. Lớp học phổ biến dành cho sinh viên MBA đang thành lập các công ty hoặcl àm trong các công ty khởi nghiệp. Nếu nhìn từ bên ngoài, khởi nghiệp là một cái gì đó rất hoang mang và không rõ ràng. Nó hỗn loạn – tới mức không theo bất kì một kế hoạch cụ thể nào. Vì không theo kế hoạch, rất khó để tìm ra cách tốt nhất để tiếp cận Vùng đất khởi nghiệp, nhận biết khi nào thì những công việc được hoàn thành,trái ngược với việc kinh doanh theo kiểu truyền thống vô cùng bài bản và có tổ chức, với một bề dày lịch sử trải dài hàng thế kỷ của các tổ chức lâu đời cùng với những cơ hội nghề nghiệp đãđượchoạchđịnhsẵn. Định vị bản thân hướng dẫn từng bước thực tế, cung cấp phân tích của người trong cuộc về các vai trò và trách nhiệm khác nhau trong công ty khởi nghiệp - bao gồm quản lý sản phẩm, tiếp thị, tăng trưởng và bán hàng - để giúp bạn biết liệu bạn có muốn tham gia công ty khởi nghiệp không và những gì đáng mong đợi nếu bạn gia nhập. Bạn sẽ hiểu rõ hơn về cách các công ty khởi nghiệp thành công hoạt động và học cách đánh giá những người bạn có thể muốn trở thành - hoặc phấn đấu. Cuốn sách trợ giúp chúng ta: - Định vị và xác định được điểm mạnh, điểm yếu của bản thân, từ đó giúp người đọc đưa ra định hướng về nghề nghiệp, vị trí để có được lựa chọn đúng đắn trong công việc. - Phân tích cấu trúc của một công ty startup và cung cấp các kỹ năng và tư duy cần thiết ở từng bộ phận. - Hiểu rõ hơn về cách các công ty khởi nghiệp thành công hoạt động và học cách đánh giá những người bạn có thể muốn trở thành - hoặc phấn đấu.', '29/11/2023', '0rst1x7i4ecqet1kzy139c2oynvm7e1r.jpg', 8, 4, 3)

insert into BOOK
values
(N'Chiến lược Marketing thời khủng hoảng', 161000, N'Trong thời đại biến động không ngừng, mọi doanh nghiệp phải đối mặt với những khủng hoảng chưa từng có. Tất cả những khó khăn từ bên ngoài tác động sâu vào hoạt động nội bộ khiến cho các doanh nghiệp có nguy cơ điêu đứng, thậm chí rơi vào trạng thái khủng hoảng. Vậy phải làm thế nào để có thể vận hành doanh nghiệp quay trở về đúng quỹ đạo của nó, tập trung phát triển hơn để có thể đứng vững trên thị trường??', '12/04/2023', '9q163dd2c3fcg1h325jpnu640u8pttd7.jpg', 7, 1, 4)

insert into BOOK
values
(N'10 bước cất cánh thương hiệu', 449000, N'10 bước xây dựng thương hiệu là cuốn sách đầu tiên và duy nhất trên thị trường về quy trình xây dựng thương hiệu viết riêng cho doanh nghiệp Việt Nam. Không chỉ là sách cẩm nang về thương hiệu mà còn là cuốn sách cầm tay chỉ việc, hướng dẫn thực hành về chiến lược.
Có thể nói, xây dựng thương hiệu luôn là vấn đề rất khó khăn với doanh nghiệp ở nhiều giai đoạn phát triển. Vì vậy, việc biên tập cuốn sách theo hành trình từng bước “cầm tay chỉ việc”, hướng dẫn cách làm cùng một số ví dụ và biểu mẫu cụ thể, sẽ giúp Doanh nghiệp có được cẩm nang xây dựng thương hiệu rõ ràng, dễ áp dụng và đem lại thành công cho doanh nghiệp.
Cuốn sách đúc kết mô hình Kim Cương gồm 6 giác cắt chính là Sứ mệnh, Tầm nhìn; Giá trị cốt lõi; Cá nhân hóa; Mô hình và Kiến trúc thương hiệu; Văn hóa thương hiệu; Lịch sử thương hiệu.
B. CUỐN SÁCH NÀY DÀNH CHO AI?
- Người khởi nghiệp, doanh nhân Việt, nhà lãnh đạo doanh nghiệp - những người đang tâm huyết xây dựng những thương hiệu vì cộng đồng, vì sự thịnh vượng chung của dân tộc Việt Nam.
- Người làm về PR & Marketing
- Sinh viên, cá nhân quan tâm đến xây dựng thương hiệu
C. CUỐN SÁCH NÀY CÓ GÌ ĐẶC BIỆT?
- Quy trình 10 bước xây dựng thương hiệu đúc kết từ kiến thức thương hiệu và quản trị của thế giới kết hợp kinh nghiệm tư vấn, đào tạo cho hàng ngàn doanh nghiệp Việt Nam trong 20 năm.
- Sở hữu quy trình và form biểu mẫu gói tư vấn chiến lược thương hiệu đang được ứng dụng hiệu quả trị giá hàng trăm triệu của Thanhs cho các doanh nghiệp Việt Nam.
- Cuốn sách “Cầm tay chỉ việc”, chi tiết, áp dụng thực tế, đầy đủ các khía cạnh cần phải hiểu rõ về thương hiệu và quá trình xây dựng thương hiệu từ 0 đến 1, từ 1 đến N....
- Hoàn thiện góc nhìn của bạn về thương hiệu. Giúp hiểu rõ vai trò của chiến lược thương hiệu trong quá trình xây dựng các cấp chiến lược và thực thi hiệu quả trong doanh nghiệp. Bạn sẽ biết làm thương hiệu có phải chỉ cần định vị, slogan hay bộ nhận diện hoành tráng hay không, năng lực cốt lõi liên quan gì...
- Tham khảo case study thực tế ứng dụng quy trình trong sách với doanh nghiêp của Việt Nam, dễ hiểu do cùng bối cảnh thị trường
D. ĐÔI NÉT VỀ TÁC GIẢ
ThS. Đặng Thanh Vân - Chuyên gia tư vấn Chiến lược Thương hiệu
- Là sáng lập viên, Chủ tịch HĐQT Công ty CP Thương hiệu và Quản trị Thanhs từ năm 2000.
- Hơn 20 năm kinh nghiệm tư vấn và giảng dạy về chiến lược thương hiệu, chiến lược marketing và kinh doanh cho các doanh nghiệp Việt Nam
- Được cộng đồng doanh nghiệp và cộng đồng Marcom Việt Nam công nhận là chuyên gia hàng đầu về Thương hiệu Việt.', '09/09/2023', 'u94wjid3f1ci6rptcxtrgqw378e1d8c8.jpg', 20, 1, 4)


insert into BOOK
values
(N'25 xu hướng công nghệ định hình cuộc Cách mạng công nghiệp 4.0', 168000, N'Ở thời đại công nghiệp 4.0 hiện nay, xu hướng công nghệ mới đang dần được công chúng tiếp cận trong cuộc sống. Dòng chảy công nghệ cứ liên tục tiếp diễn và thay đổi không ngừng nên việc nắm bắt xu hướng công nghệ mới là một điều tất yếu, nó hết sức cần thiết trong thời đại ngày nay để phát triển cá nhân, doanh nghiệp trong tương lai. 
Bernard Marr là tác giả của hơn 15 cuốn sách, hàng trăm báo cáo và ông từng cộng tác với nhiều tổ chức hàng đầu thế giới như Microsofl, Google và Liên hợp quốc.
Cuốn sách giới thiệu 25 xu hướng công nghệ cốt lõi cùng các công ty đi đầu trong các công nghệ mới như: trí tuệ nhân tạo; Internet vạn vật; dữ liệu lớn; chuỗi khối; điện toán đám mây... Các xu hướng công nghệ mới đều được áp dụng trong đời sống với cá nhân và doanh nghiệp. 
Dù ở thời đại nào thì công nghệ vẫn luôn hiện hữu, nó mang đến sức ảnh hưởng vô cùng to lớn đối với con người. Cuốn sách sẽ chỉ cho bạn đọc thấy được những công nghệ mới trong thời đại ngày nay để con người có thể áp dụng, phát triển kinh tế, mô hình kinh doanh một cách thiết thực nhất.
Xu hướng 1: Trí tuệ nhân tạo và học máy
Xu hướng 2: Internet vạn vật và sự phát triển của thiết bị thông minh 
Xu hướng 3: Từ thiết bị đeo trên người đến tăng cường năng lực con người 
Xu hướng 4: Dữ liệu lớn và phân tích tăng cường 
Xu hướng 5: Không gian và địa điểm thông minh
Xu hướng 6: Blockchain và sổ cái phân tán
Xu hướng 7: Đám mây và điện toán biên
Xu hướng 8: Thực tế mở rộng kỹ thuật số
Xu hướng 9: Bản sao số
Xu hướng 10: Xử lý ngôn ngữ tự nhiên 
Xu hướng 11: Giao diện giọng nói và chatbot
Xu hướng 12: Thị giác máy tính và nhận dạng khuôn mặt 
Xu hướng 13: Robot và robot cộng tác
Xu hướng 14: Xe tự hành
Xu hướng 15: 5G và hệ thống mạng nhanh hơn, thông minh hơn
Xu hướng 16: Di truyền học và chỉnh sửa gen
Xu hướng 17: Máy sáng tạo và thiết kế tăng cường 
Xu hướng 18: Nền tảng số
Xu hướng 19: Máy bay không người lái và phương tiện không người lái
Xu hướng 20: An ninh mạng và khả năng phục hồi mạng
Xu hướng 21: Tính toán lượng tử 
Xu hướng 22: Tự động hoá quy trình 
Xu hướng 23: Cá nhân hoá hàng loạt và khoảnh khắc tức thời
Xu hướng 24: In 3D, 4D và sản xuất bồi đắp
Xu hướng 25: Công nghệ nano và khoa học vật liệu', '12/04/2023', '40c4c22193e0a587d63d71f499919fa2.jpg', 12, 5, 1)

insert into BOOK
values
(N'Thấu hiểu khách hàng cho chiến lược kinh doanh và thực thi hiệu quả', 242000, N'Sự thấu hiểu khách hàng ở cấp độ cá nhân, thay vì coi khách hàng là một gương mặt mờ nhạt nào đó trong đám đông đại trà đã và đang trở thành một nhu cầu bắt buộc để đảm bảo sự sinh tồn cho doanh nghiệp trong bối cảnh cạnh tranh ngày nay. Bắt kịp với sự dịch chuyển này của thị trường, khái niệm customer insight (hay consumer insight) – thấu hiểu khách hàng – ra đời như một mảnh ghép hoàn hảo vào bức tranh thị trường, làm cây cầu nối giữa doanh nghiệp và tài sản lớn nhất của họ: Khách hàng.', '11/11/2022', '13sij2q2qbuzfaexbkzomv915yxiw1yc.jpg', 20, 1, 4)

insert into BOOK
values
(N'Nhật ký trong tù (Quách Tấn phỏng dịch)', 104000, N'Cuốn “Nhật ký trong tù” (bản phỏng dịch của nhà thơ Quách Tấn) do Nhà xuất bản Chính trị quốc gia Sự thật xuất bản và phát hành, là một ấn phẩm đặc sắc dành cho bạn đọc nhân dịp kỷ niệm 133 năm Ngày sinh của Chủ tịch Hồ Chí Minh (19/5/1890 – 19/5/2023). Đặc biệt, năm 2023 cũng tròn 80 năm kể từ khi Người viết tác phẩm “Ngục trung nhật ký”. 
Tác phẩm “Nhật ký trong tù” là tập thơ gồm 133 bài, viết bằng chữ Hán, ra đời trong một hoàn cảnh đặc biệt. Tháng 8/1942, Nguyễn Ái Quốc lấy tên Hồ Chí Minh với danh nghĩa là đại biểu của Việt Nam độc lập đồng minh và Phân bộ quốc tế phản xâm lược của Việt Nam sang Trung Quốc công tác. Khi đến Túc Vinh, Quảng Tây, Người bị chính quyền Tưởng Giới Thạch bắt giam vô cớ, và từ đây bắt đầu hành trình 13 tháng đầy gian nan, cực khổ trải qua 18 nhà lao của 13 huyện thuộc tỉnh Quảng Tây. Trong những tháng ngày đó (tháng 8/1942 - tháng 9/1943), Người đã sáng tác tập thơ “Ngục trung nhật ký” (Nhật ký trong tù).
Tập thơ phản ánh một cách chân thực về chế độ nhà tù và một phần xã hội Trung Quốc dưới thời Tưởng Giới Thạch. Nhà tù là nơi diễn ra nhiều tệ nạn (đánh bạc, buôn bán, hối lộ…), với bao bất công, ngang trái, đày ải, áp bức người dân trong cảnh khốn cùng. 
Mỗi bài thơ trong tập Nhật ký là tiếng lòng của tác giả, khắc họa sâu sắc tâm hồn, những suy nghĩ, tình cảm của Bác trong thời gian bị giam cầm nơi đất khách. Đó là lòng yêu nước thiết tha, luôn đau đáu hướng về Tổ quốc, mong được trở về hòa mình vào cuộc chiến đấu của đồng chí, đồng bào. Mặc dù chịu bao khổ cực, áp bức nhưng Người luôn dành tình yêu thương, sự quan tâm đến mọi người, đặc biệt là những bạn tù xung quanh. Tình yêu thương bao la, vô bờ bến của Người không chỉ dành cho mọi kiếp người, không phân biệt giai cấp, dân tộc mà còn là tình yêu thiên nhiên, hòa mình vào muôn cảnh vật. Toát lên từ toàn bộ tập Nhật ký là một tinh thần lạc quan cách mạng, niềm tin vào ngày mai tươi sáng, ý chí kiên cường, bền bỉ, lòng quyết tâm sắt đá của Người. Bản lĩnh của người chiến sĩ cộng sản, sức mạnh tinh thần lớn lao đã đưa Người vượt qua đày ải, ngục tù, đến với ngày tự do, trở về Tổ quốc thân yêu, lãnh đạo toàn dân giành độc lập, tự do cho dân tộc. Tác phẩm đã trở thành bảo vật quốc gia của Việt Nam, được bạn bè quốc tế ngợi ca và được dịch ra nhiều ngôn ngữ trên thế giới.
Với bản dịch này của nhà thơ Quách Tấn, những độc giả yêu mến “Nhật ký trong tù” có thêm một lựa chọn nữa bên cạnh bản dịch quen thuộc của Nam Trân và các bậc túc nho khác. Những trang thơ được dịch và thể hiện theo lối mới lạ, độc đáo trong ấn phẩm cho chúng ta hiểu và trân trọng hơn về tài năng dịch thuật, đặc biệt là về tình cảm của thi sĩ Quách Tấn đối với Chủ tịch Hồ Chí Minh kính yêu. Vốn được biết đến là một dịch giả thơ Đường hàng đầu của nước ta, tuy nhiên trong bản dịch này, Quách Tấn đã phá cách, chuyển một số bài của “Nhật ký trong tù” sang những thể thơ truyền thống khác của Việt Nam như thể thơ lục bát, bởi theo nhà thơ, “Có nhiều bài tôi thấy dịch thành lục bát nó ý vị hơn”. Chính vì lý do này, nên Quách Tấn đã khiêm tốn để là “phỏng dịch”.
Đặc biệt với ấn bản này, bạn đọc sẽ được “chiêm ngưỡng” những bài thơ của Bác với phần chép tay chữ Hán của nhà thư pháp Trần Thúc Lâm, một người bạn văn chương của Quách Tấn và phần chép tay chữ quốc ngữ rất đẹp của chính nhà thơ.', '10/10/2023', 'cd19aa6163295ef2dff24012f78b9aec.jpg', 30, 3, 3)

insert into BOOK
values
(N'HP 07. Harry Potter và Bảo bối tử thần', 256000, N'Harry Potter đang chuẩn bị rời khỏi nhà Dursley và đường Privet Drive trong thời khắc cuối cùng. Tuy nhiên, tương lai Harry đầy rẫy hiểm nguy, không chỉ cho cậu mà cả những người gần gũi – và Harry đã mất mát quá nhiều. Chỉ bằng cách tiêu hủy những Trường Sinh Linh Giá, Harry Potter mới có thể tự cứu mình và vượt qua những thế lực đen tối của Chúa tể hắc ám. Ở phần kết đầy kịch tính của loạt truyện Harry Potter này, Harry phải để những người bạn trung thành nhất ở lại tuyến sau để dấn thân vào cuộc hành trình nguy hiểm cuối cùng hòng tìm kiếm sức mạnh và đối mặt với số phận đáng sợ của cậu: một cuộc chiến sinh tử và đơn độc. ‘Điều hay nhất ở cuốn sách này là cuối cùng nó cũng trả lời các câu hỏi mà chúng ta khao khát được biết.’ - Daily Mirror', '14/11/2021', 'sl28p0tvoylznvinccj42l1ag2qqqrsv.jpg', 10, 3, 3)

insert into BOOK
values
(N'Phương Pháp Quản Lý Nhân Sự Trong Công Ty', 69000, N'Những người sử dụng lao động đã hiểu dần các giá trị mà con người tạo ra cho tổ chức của họ. ', '03-03-2022', 'book01.jpg', 10, 1, 2),
(N'Hành Vi Tổ Chức - Organizational Behavior', 75000, N'"Vũ trụ cũng không khó hiểu bằng hành động của con người." Marcel Proust Tiến sĩ Timothy A. Judge từng là giáo sư giảng dạy tại trường Đại Học Cornell và đại học Lowa. Ông nghiên cứu chuyên sâu vào các lĩnh vực liên quan đến sự khác biệt cá nhân, thuật lãnh đạo và ảnh hưởng hành vi con người...Ông đã được trao nhiều giải thưởng quan trọng như Emest J.McCormick Award, Larry L. Cummings Award... ', '03-03-2022', 'book02.jpg', 10, 1, 1),
(N'17 Nguyên Tắc Vàng Trong Làm Việc Nhóm', 75000, N'Khi trở thành thành viên của một nhóm nào đó thì vấn đề bạn cần băn khoăn không phải là “Có nên tham gia các hoạt động của nhóm không?” mà là “Những đóng góp của bạn có mang lại thành công cho nhóm không?” Các cá nhân không thể gắn kết với nhau là nguyên nhân làm tan rã nhóm. Một số người nghĩ rằng chìa khoá để thành công là nguyên tắc làm việc rõ ràng. Nhưng trên thực tế, có nhiều người rất siêng năng, khả năng làm việc độc lập rất tốt nhưng lại không thể làm việc cùng nhau để phát huy hết tiềm lực của họ. Thực chất, nhóm phải là nhóm những cá nhân luôn tương trợ, giúp đỡ lẫn nhau, tạo động lực cho nhau phát triển. Giữa các cá nhân phải có sự tương tác với nhau như một chuỗi phản ứng hoá học. Đó là cách thức để xây dựng nhóm của riêng bạn.  ', '03-03-2022', 'book03.jpg', 20, 1, 2)

insert into BOOK
values
(N'Bình Luận Khoa Học Bộ Luật Tố Tụng Hình Sự năm 2015 (Tái bản lần thứ hai, có chỉnh sửa, bổ sung)', 350000, N'Cuốn sách : "Bình luận khoa học Bộ luật Tố tụng hình sự năm 2015" do TS. Phạm Mạnh Hùng và các tác giả đang công tác tại Viện kiểm sát nhân dân Tối Cao và Trường Đại học kiểm sát Hà Nội biên soạn. Cuốn sách  không chỉ hướng tới phục vụ các cá nhân, cơ quan , tổ chức trong việc chấp hành và áp dụng pháp luật Tố tụng hình sự mà còn hỗ trợ các nhà nghiên cứu, giảng viên, học viên, sinh viên trong công tác nghiên cứu, giảng dạy và học tập khoa học luật Tố tụng hình sự.', '11/12/2020', 'vzgf5bu3if65wo3e5cthbpwmrcqesstt.jpg', 10, 2, 2)

insert into BOOK
values
(N'Lãnh thổ Việt Nam Lịch sử và pháp lý', 168000, N'Cuốn sách góp phần truyền đạt thêm nhiều thông tin chính xác và những bài học bổ ích cho bạn đọc quan tâm, đặc biệt là các thế hệ trẻ đang kế tục gánh vác sứ mệnh kiên quyết đấu tranh gìn giữ sự toàn vẹn lãnh thổ quốc gia, bảo vệ chủ quyền và các quyền hợp pháp trong phạm vi lãnh thổ đó, đồng thời, kiên trì giải quyết mọi bất đồng, tranh  chấp nhằm phục vụ cho mục tiêu góp phần giữ vững hòa bình, ổn định, hợp tác và phát triển của khu vực và quốc tế trong bối cảnh hiện nay.', '15/09/2020', 'c3uu5cno6sli9s89ezuafejqhn0vdntw.jpg', 5, 2, 2)

--- TABLE CLIENT ---

insert into CLIENT
values
(N'Nguyễn Văn An', N'Nam', '14-10-2003', '0367353927', N'236 Điện Biên Phủ, Quận Bình Thạnh, TP.HCM', N'just4fun@gmail.com', N'vanan', N'123456'),
(N'Nguyễn Quốc Dũng', N'Nam', '22-10-2004', '0367353922', N'140 Lê Trọng Tấn, Quận Tân Phú, TP.HCM', N'nhl@gmail.com', N'quocdung', N'123'),
(N'Thùy Dung', N'Nữ', '11-10-2023', '0973711953', N'26 Hùng Vương, TP.Pleiku, Gia lai', N'thuydung@gmail.com', N'thuydung', N'123456')

--- TABLE [ORDER] ---

insert into [ORDER]
values
('30/11/2023',null, N'Chưa thanh toán', N'Chờ xác nhận', 1),
('29/11/2023',null, N'Chưa thanh toán', N'Chờ xác nhận', 1),
('29/11/2023',null, N'Chưa thanh toán', N'Chờ xác nhận', 2)

--- TABLE ORDER_DETAIL ---
insert into ORDER_DETAIL
values
(1, 6, 1, 168000),
(2, 6, 1, 168000),
(2, 9, 2, 449000),
(3, 8, 3, 161000)


select * from ROLES
select * from [USER]
select * from CATEGORY
select * from PUBLISHER
select * from BOOK
select * from CLIENT
select * from [ORDER]
select * from ORDER_DETAIL









