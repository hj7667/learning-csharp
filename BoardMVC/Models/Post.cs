using System.ComponentModel.DataAnnotations;

namespace Section26_BoardMVC.Models
{
    public class Post
    {
        public int Id { get; set; } // 관례상 "Id"는 자동으로 기본키(PK)로 인식됨

        [Required(ErrorMessage = "제목을 입력해주세요.")]
        [MaxLength(100, ErrorMessage = "제목은 100자를 넘을 수 없습니다.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "내용을 입력해주세요.")]
        public string Content { get; set; } = "";

        [Required(ErrorMessage = "작성자를 입력해주세요.")]
        [MaxLength(20)]
        public string Writer { get; set; } = "";

        // [BindNever]: 클라이언트가 폼으로 이 값을 보내도 모델 바인딩 시 무시함
        // → 작성일은 서버가 직접 채워야지, 사용자가 조작하면 안 되는 값이라 오버포스팅 방지 차원
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}