import { getProductImage } from "../productService";
import { useLoadData } from "@/misc";

export default function ProductImage({ id, className }) {
  const { data: imageSrc, loading } = useLoadData(getProductImage, id);

  return loading ? (
    <div>Loading...</div>
  ) : (
    imageSrc && <img className={className} src={imageSrc} alt="product image" />
  );
}
